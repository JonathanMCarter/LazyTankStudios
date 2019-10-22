using System.Collections;
using UnityEngine;

namespace Test
{
    public class TestTimeAction
    {
        private ActionOverTime action;
        private float elapsedTime;
        private float expiryTime;

        public TestTimeAction(float time)
        {
            action = ScriptableObject.CreateInstance<ActionOverTime>();
            action.ActionDelegate = DebugAction;
            action.ExpiryTime = time;
            expiryTime = time;
        }

        public IEnumerator Execute(MonoBehaviour mb)
        {
            yield return action.Use(mb, Time.deltaTime);
        }

        private void DebugAction(MonoBehaviour mb, float deltaTime)
        {
            elapsedTime += deltaTime;
            if(elapsedTime >= expiryTime)
                Debug.Log("Test");
        }

    } 
}
