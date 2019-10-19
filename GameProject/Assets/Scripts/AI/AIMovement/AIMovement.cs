using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AI Movement Script
 * 
 * 
 * 
 * Owner: ???
 * Last Edit : 
 * 
 * Also Edited by : Tony Parsons
 * Last Edit: 12.10.19
 * Reason: Made it so enemy does the dying
 * 
 * */

namespace AI
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class AIMovement : MonoBehaviour
    {
        [SerializeField] private int xRange;
        [SerializeField] private int yRange;
        [SerializeField] bool showGizmos;
        [SerializeField] float movementSpeed = 2f;
        [SerializeField] float maxIdleTime = 2f;
        [SerializeField] float awarnessSize = .75f;

        private GameObject player;

        //Tony Edit
        public SpriteRenderer[] Hearts;
        private int Health = 2;
        //private BoxCollider2D HeroAttackThing;
        //End of edit
        private FiniteStateMachine fsm;
        private TaskOverTime tot;
        private Vector2 rootPos;

        public Vector2 RootPos => rootPos;
        public int XRange => xRange;
        public int YRange => yRange;
        public float MovementSpeed => movementSpeed;

        public bool IsMoving { get; private set; }
        public bool IsReadyToMove { get; private set; }

        Coroutine Fix;

        // Start is called before the first frame update
        void Awake()
        {
            player  = FindObjectOfType<PlayerMovement>().gameObject;
            //HeroAttackThing = player.GetComponent<BoxCollider2D>();
            rootPos = transform.position;
            CircleCollider2D c = GetComponent<CircleCollider2D>();
            c.radius = awarnessSize;
            c.isTrigger = true;
            tot = new TaskOverTime(this);
            IsReadyToMove = true;
            fsm = new FiniteStateMachine(this, new RandomWanderState(this));
        }

        // Update is called once per frame
        void Update()
        {
            fsm.ExecuteCurrentState();
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(UnityEditor.EditorApplication.isPlaying ? (Vector3)rootPos : transform.position, new Vector3(xRange, yRange));

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, awarnessSize);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            tot.Stop();

            if (collision.tag == "Player")
            {
                fsm.ChangeState(new ChasePlayerState(this, collision.gameObject));
                player = collision.gameObject;


                //Tony Edit
                for (int i = 0; i < Health; ++i)
                    Hearts[i].gameObject.SetActive(true);
                //end of edit
            }

            //Tony Edit
            //if (collision == HeroAttackThing)
            if (collision.gameObject.tag == "Bullet")
            {
                Hearts[Health - 1].gameObject.SetActive(false);
                --Health;
                //if (Health <= 0)
                //    this.gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            //Cache States someday


            if (Fix == null)
                Fix = StartCoroutine(StopBug());
            for (int i = 0; i < Health; ++i)
                Hearts[i].gameObject.SetActive(false);
        }

        public void RandomWander(Vector2 destination)
        {
            IsMoving = true;
            IsReadyToMove = false;
            Vector2 start = transform.position;
            tot.Start((destination - start).magnitude / movementSpeed, (float progress) => transform.position = Vector2.Lerp(start, destination, progress), Idle);
        }

        public void ChasePlayer()
        {
            if ((player.transform.position - transform.position).magnitude > 0.5f)
            {
                transform.position += (player.transform.position - transform.position).normalized * movementSpeed * Time.deltaTime;
            }
        }

        private void Idle()
        {
            IsMoving = false;
            tot.Start(Random.Range(0, maxIdleTime), (float f) => { }, () => { IsReadyToMove = true; });
        }


        // Jonathan Added this to stop the annoying bouncing of the sprite for no reason!!!!!!
        private IEnumerator StopBug()
        {
            yield return new WaitForSeconds(XRange / 2);
            fsm.ChangeState(new RandomWanderState(this));
            Fix = null;
        }
    }
}
