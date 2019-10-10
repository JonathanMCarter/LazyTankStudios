using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIMovement : MonoBehaviour
    {
        [SerializeField] private int xRange;
        [SerializeField] private int yRange;
        [SerializeField] bool showGizmos;
        [SerializeField] float movementSpeed = 2f;
        [SerializeField] float maxIdleTime = 2f;
        [SerializeField] float awarnessSize = .75f;

        [SerializeField] Player player;

        private FiniteStateMachine fsm;
        private TaskOverTime tot;
        private Vector2 rootPos;

        public Vector2 RootPos => rootPos;
        public int XRange => xRange;
        public int YRange => yRange;
        public float MovementSpeed => movementSpeed;

        public bool IsMoving { get; private set; }
        public bool IsReadyToMove { get; private set; }

        // Start is called before the first frame update
        void Awake()
        {
            rootPos = transform.position;
            GetComponent<CircleCollider2D>().radius = awarnessSize;
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
            Gizmos.DrawWireSphere(transform.position, awarnessSize *3); // Wtf? Why * 3?? Works.
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            tot.Stop();
            fsm.ChangeState(new ChasePlayerState(this, player));
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            //Cache States someday
            fsm.ChangeState(new RandomWanderState(this));
        }

        private void Idle()
        {
            IsMoving = false;
            tot.Start(Random.Range(0, maxIdleTime), (float f) => { }, () => { IsReadyToMove = true; });
        }
    }
}
