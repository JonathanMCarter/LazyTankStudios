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
        [SerializeField] private List<Action> actions = new List<Action>();

        private FiniteStateMachine fsm;
        private TaskOverTime tot;
        private Vector2 rootPos;

        public Vector2 RootPos => rootPos;
        public int XRange => xRange;
        public int YRange => yRange;
        public float MovementSpeed => movementSpeed;

        public float MaxIdleTime { get => maxIdleTime; private set=>maxIdleTime = value; }

        // Start is called before the first frame update
        void Awake()
        {
            rootPos = transform.position;
            GetComponent<CircleCollider2D>().radius = awarnessSize;
            tot = new TaskOverTime(this);
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
            Gizmos.DrawWireSphere(transform.position, awarnessSize); // Wtf? Why * 3?? Works.
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            Debug.Log("Collison");
            tot.Stop();

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            //Cache States someday
            tot.Stop();
            fsm.ChangeState(new RandomWanderState(this));
        }
        
        public void Move(Vector2 destination, CallbackDel reachedTarget =null)
        {
            Vector2 start = transform.position;
            tot.Start((destination - start).magnitude / movementSpeed, (float progress) => transform.position = Vector2.Lerp(start, destination, progress), reachedTarget);
        }
    }
}
