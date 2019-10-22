using UnityEngine;

namespace AI
{
    public abstract class State
    {
        public State(MonoBehaviour owner) { }
        public abstract void StateEnter();
        public abstract void Execute();
        public abstract void StateExit();
    } 
}