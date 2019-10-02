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
        [SerializeField] bool showBox;
        [SerializeField] float movementSpeed = 2f;
        [SerializeField] float maxIdleTime = 2f;

        private FiniteStateMachine fsm;
        private TaskOverTime tot;

        public Vector2 RootPos => rootPos;
        public int XRange => xRange;
        public int YRange => yRange;

        public bool IsMoving { get; private set; }
        public bool IsReadyToMove { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            tot = new TaskOverTime(this);
            fsm = new FiniteStateMachine(this, new RandomWanderState(this));
            IsReadyToMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            fsm.ExecuteCurrentState();
        }

        private void OnDrawGizmos()
        {
            if (!showBox) return;
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(rootPos, new Vector3(xRange, yRange));
        }

        public void MoveAccess(Vector2 destination)
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
