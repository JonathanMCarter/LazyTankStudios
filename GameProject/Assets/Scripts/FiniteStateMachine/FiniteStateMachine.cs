using UnityEngine;

namespace AI
{
    public class FiniteStateMachine
    {
        private MonoBehaviour _owner;
        private State _currentState;

        public FiniteStateMachine(MonoBehaviour owner, State initState)
        {
            _owner = owner;
            _currentState = initState;
            _currentState.StateEnter();
        }

        public void ExecuteCurrentState() => _currentState?.Execute();

        public void ChangeState(State newState)
        {
            _currentState?.StateExit();

            _currentState = newState;

            _currentState?.StateExit();
        }
    }

}