using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * AI Movement Script
 * 
 * 
 * 
 * Owner: Tom Last
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
    public class TestAIMovement : MonoBehaviour
    {
        [SerializeField] private int xRange;
        [SerializeField] private int yRange;
        [SerializeField] bool showGizmos;
        [SerializeField] float movementSpeed = 2f;
        [SerializeField] float maxIdleTime = 2f;
        [SerializeField] Vector2 awarnessSize = Vector2.one;
        [SerializeField] protected PlayerVariable player;

        protected IEnumerator currentAction;
        private FiniteStateMachine fsm;
        private TaskOverTime tot;
        private Vector2 rootPos;


        public Vector2 RootPos => rootPos;
        public int XRange => xRange;
        public int YRange => yRange;
        public float MovementSpeed => movementSpeed;

        public bool IsMoving { get; private set; }
        public bool IsReadyToMove { get; private set; }

        
        void Awake()
        {
            rootPos = transform.position;
            BoxCollider2D c = GetComponent<BoxCollider2D>();
            c.size = awarnessSize;
            c.isTrigger = true;
            tot = new TaskOverTime(this);
            IsReadyToMove = true;
            fsm = new FiniteStateMachine(this, new TestRandomWanderState(this));
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            fsm.ExecuteCurrentState();
        }

        private void OnDrawGizmos()
        {
            if (!showGizmos) return;
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(UnityEditor.EditorApplication.isPlaying ? (Vector3)rootPos : transform.position, new Vector3(xRange, yRange));

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, awarnessSize);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            tot.Stop();
            fsm.ChangeState(null);
        }

        protected virtual void OnTriggerExit2D(Collider2D other)
        {
            //Cache States someday
            tot.Stop();
            fsm.ChangeState(new TestRandomWanderState(this));

        }

        public void RandomWander(Vector2 destination)
        {
            IsMoving = true;
            IsReadyToMove = false;
            Vector2 start = transform.position;
            tot.Start((destination - start).magnitude / movementSpeed, (float progress) => transform.position = Vector2.Lerp(start, destination, progress), Idle);
        }

        private void Idle()
        {
            IsMoving = false;
            tot.Start(Random.Range(0, maxIdleTime), (float f) => { }, () => { IsReadyToMove = true; });
        }
    }
}