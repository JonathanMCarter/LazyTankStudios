using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] Vector2 patrollingRange;
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
        public bool playerInRange = false;
        private float patrolTime = 0f;
        private float abilityTime = 0f;
        private float patrollTimer = 3f;
        private float abilityTimer = 3f;
        private Vector3 targetPos;
        private Ability[] abilities;
        private Ability currentAbility;

        private void Awake()
        {
            player = FindObjectOfType<PlayerMovement>();
            playerInventory = FindObjectOfType<Inventory>();
            rootPos = transform.position;
            abilities = GetComponents<Ability>();
            try { myAnim = GetComponent<Animator>(); }
            catch { }
        }

        private void Update()
        {
            if (!playerInRange)
                Patroll();
            else if (!(player.transform.position.x >= (-maxChasingArea.x + rootPos.x) && player.transform.position.x <= (maxChasingArea.x + rootPos.x) &&
                player.transform.position.y >= (-maxChasingArea.y + rootPos.y) && player.transform.position.y <= (maxChasingArea.y + rootPos.y)))
            {
                playerInRange = false;
                currentAbility?.Interrupt();
            }
            else
            {
                UseAbility();
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

            Gizmos.DrawWireCube(UnityEditor.EditorApplication.isPlaying ? (Vector3)rootPos : transform.position, patrollingRange);

            Gizmos.color = Color.blue;

            Gizmos.DrawWireCube(UnityEditor.EditorApplication.isPlaying ? (Vector3)rootPos : transform.position, maxChasingArea * 2);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                Debug.Log("Detected");
                playerInRange = true;
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

            if (!HeartsCoRunning)
            {
                HeartsShowing = false;
                StartCoroutine(HeartsCo());
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!gameObject.activeInHierarchy) return; //Added by LC

            if (!HeartsCoRunning)
            {
                HeartsShowing = true;
                StartCoroutine(HeartsCo());
            }
        }

        private void Patroll()
        {
            if(patrolTime >= 0f)
            {
                patrolTime = -Random.Range(1f, patrollTimer);
                targetPos = rootPos + new Vector2(Random.Range(-patrollingRange.x / 2, patrollingRange.x / 2), Random.Range(-patrollingRange.y / 2, patrollingRange.y / 2));
            }
            transform.position += (targetPos - transform.position).normalized * Time.deltaTime * movementSpeed;
            patrolTime += Time.deltaTime;
        }

        private void UseAbility()
        {
            if(abilityTime >= 0f)
            {
                currentAbility = abilities[Random.Range(0, abilities.Length - 1)];
                abilityTime = -currentAbility.CastTime;
                currentAbility.Interrupt();
                currentAbility.Use();
            }
            abilityTime += Time.deltaTime;

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
    } 
}
