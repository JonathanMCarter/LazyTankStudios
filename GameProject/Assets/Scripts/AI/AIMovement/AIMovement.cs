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
    [RequireComponent(typeof(BoxCollider2D))]
    public class AIMovement : MonoBehaviour
    {
        [SerializeField] private int xRange;
        [SerializeField] private int yRange;
        [SerializeField] bool showGizmos;
        [SerializeField] float movementSpeed = 2f;
        [SerializeField] float maxIdleTime = 2f;
        [SerializeField] Vector2 awarnessSize = Vector2.one;

        private GameObject player;

        //Tony Edit
        public SpriteRenderer[] Hearts;
        public int Health = 2;
        //private BoxCollider2D HeroAttackThing;
        //End of edit
        private FiniteStateMachine fsm;
        private TaskOverTime tot;
        private Vector2 rootPos;

        // Jonathan Addition
        private bool HeartsShowing;
        private bool HeartsCoRunning = false;

        public Vector2 RootPos => rootPos;
        public int XRange => xRange;
        public int YRange => yRange;
        public float MovementSpeed => movementSpeed;

        public bool IsMoving { get; private set; }
        public bool IsReadyToMove { get; private set; }

        Coroutine Fix;
        //Andreas edit--
        private Animator myAnim;

        // Start is called before the first frame update
        void Awake()
        {
            player  = FindObjectOfType<PlayerMovement>().gameObject;
            //HeroAttackThing = player.GetComponent<BoxCollider2D>();
            rootPos = transform.position;
            BoxCollider2D c = GetComponent<BoxCollider2D>();
            c.size = awarnessSize;
            c.isTrigger = true;
            tot = new TaskOverTime(this);
            IsReadyToMove = true;
            fsm = new FiniteStateMachine(this, new RandomWanderState(this));
            //Andreas edit--
            try{myAnim=GetComponent<Animator>();}
            catch{}
        }

        // Update is called once per frame
        void Update()
        {
            fsm.ExecuteCurrentState();

            if (Health <= 0)
            {
                //Destroy(this.gameObject);
                gameObject.SetActive(false);
            }
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(UnityEditor.EditorApplication.isPlaying ? (Vector3)rootPos : transform.position, new Vector3(xRange, yRange));

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, awarnessSize);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            //if (collision.tag == "Player") //Need to get gameObject of collision. Comment added by LC
            if (collision.gameObject.CompareTag("Player"))
            {
                fsm.ChangeState(new ChasePlayerState(this, collision.gameObject));
                player = collision.gameObject; //why do we need to store player info here if not reusing it? comment added by LC
                collision.GetComponent<PlayerMovement>().TakeDamage(1); //added by LC temp for prototype until abilities working


                //Tony Edit
                //for (int i = 0; i < Health; ++i)
                //    Hearts[i].gameObject.SetActive(true);
                //end of edit

                if (!HeartsCoRunning)
                {
                    HeartsShowing = false;
                    StartCoroutine(HeartsCo());
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Fix != null)
            {
                StopCoroutine(Fix);
                Fix = null;
            }
            tot.Stop();
            ////////////////////////Section moved to OnTriggerStay//////////////////
            ////if (collision.tag == "Player") //Need to get gameObject of collision. Comment added by LC
            //if (collision.gameObject.CompareTag("Player"))
            //{
            //    print("PlayerCol");
            //    fsm.ChangeState(new ChasePlayerState(this, collision.gameObject));
            //    player = collision.gameObject; //why do we need to store player info here if not reusing it? comment added by LC
            //    collision.GetComponent<PlayerMovement>().TakeDamage(1); //added by LC temp for prototype until abilities working


            //    //Tony Edit
            //    //for (int i = 0; i < Health; ++i)
            //    //    Hearts[i].gameObject.SetActive(true);
            //    //end of edit

            //    if (!HeartsCoRunning)
            //    {
            //        HeartsShowing = false;
            //        StartCoroutine(HeartsCo());
            //    }
            //}
            /////////////////////////////////////////////////////////////////////////////

            //Tony Edit
            //if (collision == HeroAttackThing)
            if (collision.gameObject.tag == "Bullet")
            {
                Destroy(collision.gameObject);

                Debug.Log("********** Enemy Should Be Taking Damage Now...");

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


            if (!HeartsCoRunning)
            {
                HeartsShowing = true;
                StartCoroutine(HeartsCo());
            }
        }

        public void RandomWander(Vector2 destination)
        {
            IsMoving = true;
            IsReadyToMove = false;
            Vector2 start = transform.position;
            tot.Start((destination - start).magnitude / movementSpeed, (float progress) => transform.position = Vector2.Lerp(start, destination, progress), Idle);
            try{
            myAnim.SetFloat("SpeedX", Mathf.Abs((destination - start).x));
            myAnim.SetFloat("SpeedY", (destination - start).y);
            }
            catch{}
        }

        public void ChasePlayer()
        {
            if ((player.transform.position - transform.position).magnitude > 0.5f)
            {
                transform.position += (player.transform.position - transform.position).normalized * movementSpeed * Time.deltaTime;
            }
            try{
            myAnim.SetFloat("SpeedX", Mathf.Abs((player.transform.position - transform.position).normalized.x));
            myAnim.SetFloat("SpeedY", (player.transform.position - transform.position).normalized.y);
            }
            catch{}
        }

        private void Idle()
        {
            IsMoving = false;
            tot.Start(Random.Range(0, maxIdleTime), (float f) => { }, () => { IsReadyToMove = true; });
        }


        // Jonathan Added this to stop the annoying bouncing of the sprite for no reason!!!!!!
        // well it kinda works somme of the time, keep it in and working on making it so AI has delays to its thinking so it doesn't keep breaking like this...
        private IEnumerator StopBug()
        {
            IsMoving = false;
            yield return new WaitForSeconds(XRange / 2);
            fsm.ChangeState(new RandomWanderState(this));
            Fix = null;
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
