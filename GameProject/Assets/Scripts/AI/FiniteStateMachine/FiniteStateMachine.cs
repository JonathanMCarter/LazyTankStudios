using UnityEngine;

namespace AI
{
    public class FiniteStateMachine
    {
        private MonoBehaviour _owner;

        public State _CurrentState { get; private set; }

        public FiniteStateMachine(MonoBehaviour owner, State initState)
        {
            _owner = owner;
            _CurrentState = initState;
            _CurrentState.StateEnter();
        }

        public void ExecuteCurrentState() => _CurrentState?.Execute();

        public void ChangeState(State newState)
        {
            _CurrentState?.StateExit();

            _CurrentState = newState;

            _CurrentState?.StateEnter();
        }
    }

}