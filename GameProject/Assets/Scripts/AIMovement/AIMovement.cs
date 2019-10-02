using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AIMovement : MonoBehaviour
    {
        [SerializeField] private int xRange;
        [SerializeField] private int yRange;
        [SerializeField] private Vector2 rootPos;
        [SerializeField] bool showGizmos;
        [SerializeField] float movementSpeed = 2f;
        [SerializeField] float maxIdleTime = 2f;
        [SerializeField] float awarnessSize = .75f;

        [SerializeField] Player player;

        private FiniteStateMachine fsm;
        private TaskOverTime tot;

        public Vector2 RootPos => rootPos;
        public int XRange => xRange;
        public int YRange => yRange;

        public bool IsMoving { get; private set; }
        public bool IsReadyToMove { get; private set; }
        public float MovementSpeed => movementSpeed;

        // Start is called before the first frame update
        void Awake()
        {
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

            Gizmos.DrawWireCube(rootPos, new Vector3(xRange, yRange));

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
            Debug.Log("Exit");
            fsm.ChangeState(new RandomWanderState(this));
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
                transform.position += (player.transform.position - transform.position).normalized * movementSpeed * Time.deltaTime;
        }

        private void Idle()
        {
            IsMoving = false;
            tot.Start(Random.Range(0, maxIdleTime), (float f) => { }, () => { IsReadyToMove = true; });
        }
    }
}
