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

        public Vector2 RootPos => rootPos;
        public int XRange => xRange;
        public int YRange => yRange;

        public bool IsMoving { get; private set; }
        public bool IsIdle { get; private set; }

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log(RootPos);
            fsm = new FiniteStateMachine(this, new RandomWanderState(this));
            IsIdle = true;
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
            StartCoroutine(Move(destination));
        }

        public IEnumerator Move(Vector2 destination)
        {
            IsMoving = true;
            Vector2 startPos = transform.position;
            float progress = 0f;
            float time = (destination - startPos).magnitude / movementSpeed;
            float timer = 0f;

            while (progress <= 1f)
            {
                timer += Time.deltaTime;
                transform.position = Vector2.Lerp(startPos, destination, progress);
                progress += Time.deltaTime / time;

                yield return null;
            }
            transform.position = destination;
            IsMoving = false;
            StartCoroutine(Idle(Random.Range(0,maxIdleTime)));
            //Debug.Log(timer);
        }

        public IEnumerator Idle(float duration)
        {
            IsIdle = false;
            float progress = 0f;

            while(progress < 1f)
            {
                progress += Time.deltaTime / duration;
                yield return null;
            }
            IsIdle = true;
        }

    } 
}
