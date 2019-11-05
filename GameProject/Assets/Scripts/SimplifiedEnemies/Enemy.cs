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
        [SerializeField] private PlayerVariable player;

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
            abilities = GetComponents<Ability>();
            rootPos = transform.position;
            aot = new ActionOverTime();
            maxChaseRect = new Rect(transform.position, maxChasingArea);
        }

        private void Start()
        {
        }

        private void Update()
        {
            time += Time.deltaTime;
            collisionTimer -= collisionTimer <= 0f ? 0 : Time.deltaTime;
            if (isPatrolling)
            {
                Patrolling();
            }
            else
            {
                if (!(player.Value.transform.position.x >= (-maxChasingArea.x + rootPos.x) && player.Value.transform.position.x <= (maxChasingArea.x + rootPos.x) &&
                    player.Value.transform.position.y >= (-maxChasingArea.y + rootPos.y) && player.Value.transform.position.y <= (maxChasingArea.y + rootPos.y)))
                {
                    Debug.Log("Stopping");
                    StopCoroutine(abilityCoroutine);
                    isPatrolling = true;
                    isReadyToMove = true;
                    return;
                }
                if (time > abilityExpiryTime)
                {
                    UseAbility();
                }
            }
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
                isPatrolling = false;
            }

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Player")
            {
                player.Value.Health--;                
            }
            if (currentAbility is Charge)
            {
                StopCoroutine(abilityCoroutine);
                time = 10f;
            }
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.transform.tag == "Player")
            {
                if (collisionTimer == 0)
                {
                    player.Value.Health--;
                    collisionTimer = 1f;
                }
            }
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