using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int xRange;
        [SerializeField] private int yRange;
        [SerializeField] bool showGizmos;
        [SerializeField] float movementSpeed = 2f;
        [SerializeField] float maxIdleTime = 2f;
        [SerializeField] private Vector2 maxChasingArea = Vector2.one;

        public SpriteRenderer[] Hearts;
        public int Health = 2;
        private bool HeartsShowing;
        private bool HeartsCoRunning = false;
        //Andreas edit--
        private Animator myAnim;

        private PlayerMovement player;
        private Inventory playerInventory;
        private Vector2 rootPos;
        private Ability[] abilities;
        private Ability currentAbility;
        private IEnumerator abilityCoroutine;
        private float abilityExpiryTime;
        private float time;
        private ActionOverTime aot;
        private bool isPatrolling = true;
        private bool isReadyToMove = true;
        private Rect maxChaseRect;
        private float collisionTimer = 0f;

        private void Awake()
        {
            player = FindObjectOfType<PlayerMovement>();
            playerInventory = GameObject.FindGameObjectWithTag("Inv").GetComponent<Inventory>();
            abilities = GetComponents<Ability>();
            rootPos = transform.position;
            aot = new ActionOverTime();
            maxChaseRect = new Rect(transform.position, maxChasingArea);
            try { myAnim = GetComponent<Animator>(); }
            catch { }
        }

        private void Start()
        {
        }

        private void Update()
        {
            if (Health <= 0)
            {
                //Destroy(this.gameObject);
                gameObject.SetActive(false);
            }
            time += Time.deltaTime;
            collisionTimer -= collisionTimer <= 0f ? 0 : Time.deltaTime;
            if (isPatrolling)
            {
                Patrolling();
            }
            else
            {
                if (!(player.transform.position.x >= (-maxChasingArea.x + rootPos.x) && player.transform.position.x <= (maxChasingArea.x + rootPos.x) &&
                    player.transform.position.y >= (-maxChasingArea.y + rootPos.y) && player.transform.position.y <= (maxChasingArea.y + rootPos.y)))
                {
                    StopCoroutine(abilityCoroutine);
                    isPatrolling = true;
                    isReadyToMove = true;
                    return;
                }
                if (time > abilityExpiryTime)
                {
                    Debug.Log("Use Ability");
                    UseAbility();
                }
            }
        }
        private void LateUpdate()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.2f); //Added by LC as temp fix as sprite rendered behind floor
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(UnityEditor.EditorApplication.isPlaying ? (Vector3)rootPos : transform.position, new Vector3(xRange, yRange));

            Gizmos.color = Color.blue;

            Gizmos.DrawWireCube(UnityEditor.EditorApplication.isPlaying ? (Vector3)rootPos : transform.position, maxChasingArea * 2);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Debug.Log("Detected");
                isPatrolling = false;
            }

            if (collision.gameObject.tag == "Bullet")
            {
                //Toby: get bullet damage instead of always 1
                Bullet b = collision.gameObject.GetComponent<Bullet>();
                int damage = b.Damage;
                playerInventory.addXP(b.SourceItem, 1);
                
                Destroy(collision.gameObject);

                // Debug.Log("********** Enemy Should Be Taking Damage Now...");

                if (Health > 0) Hearts[Health - 1].gameObject.SetActive(false); //if statement added by LC to avoid potential errors
                Health -= damage;
                //if (Health <= 0)
                //    this.gameObject.SetActive(false);
            }
            if (collision.gameObject.tag == "Sword")
            {
                Bullet b = collision.gameObject.GetComponent<Bullet>();
                int damage = b.Damage;
                playerInventory.addXP(b.SourceItem, 1);
                // Debug.Log("********** Enemy Should Be Taking Damage Now...");

                if (Health > 0) Hearts[Health - 1].gameObject.SetActive(false);
                Health -= damage;
                //if (Health <= 0)
                //    this.gameObject.SetActive(false);
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerMovement>() != null)
            {
                player.TakeDamage(1);               
            }
            if (currentAbility is Charge)
            {
                StopCoroutine(abilityCoroutine);
                time = 10f;
            }
            if (!HeartsCoRunning)
            {
                HeartsShowing = false;
                StartCoroutine(HeartsCo());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            //Cache States someday

            if (!gameObject.activeInHierarchy) return; //Added by LC


            if (!HeartsCoRunning)
            {
                HeartsShowing = true;
                StartCoroutine(HeartsCo());
            }
        }


        // Makes it so the hearts don't glitch in and out of exsistance
        // granted the way its codes does work... just not well enough... this just fixes that a little....
        private IEnumerator HeartsCo()
        {
            HeartsCoRunning = true;

            if ((HeartsShowing) && (Health > 0))
            {
                for (int i = 0; i < Health; ++i)
                {
                    // Hearts[i].gameObject.SetActive(false); commented out by LC - Was causing hearts to always be inactive after a hit
                    HeartsShowing = false;
                }
            }
            else
            {
                for (int i = 0; i < Health; ++i)
                {
                    Hearts[i].gameObject.SetActive(true);
                    HeartsShowing = true;
                }
            }

            yield return new WaitForSeconds(.25f);

            HeartsCoRunning = false;
        }

        public void Patrolling()
        {
            if (isReadyToMove)
            {
                isReadyToMove = false;
                StartCoroutine(Move());
            }
        }

        public IEnumerator Move()
        {
            time = 0f;
            Vector2 start = transform.position;
            Vector2 destination = GetDestination();
            aot.ExpiryTime = (destination - start).magnitude / movementSpeed;
            aot.ActionDelegate = () => { transform.position = Vector2.Lerp(start, destination, time / aot.ExpiryTime); };
            yield return StartCoroutine(aot.Use());
            yield return new WaitForSeconds(maxIdleTime);
            isReadyToMove = true;
        }

        private Vector2 GetDestination() => new Vector2(Random.Range(rootPos.x - xRange / 2, rootPos.x + xRange / 2), Random.Range(rootPos.y - yRange / 2, rootPos.y + yRange / 2));

        private void UseAbility()
        {
            int r = Random.Range(0, abilities.Length);
            currentAbility = abilities[r];
            abilityCoroutine = currentAbility.Use();
            abilityExpiryTime = abilities[r].ExpiryTime;
            time = 0f;
            StartCoroutine(abilityCoroutine);
        }
    }
}