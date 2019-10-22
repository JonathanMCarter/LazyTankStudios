using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Monster : MonoBehaviour
    {
        public GameObject target;

        private Test.ChaseAction action;
        private IEnumerator currentAction;
        // Start is called before the first frame update
        void Start()
        {
            action = ScriptableObject.CreateInstance<ChaseAction>();
            action.Init(target.transform, 10f, 2f);
            UseAction();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(!currentAction.MoveNext())
                {
                    Debug.Log("Use");
                    UseAction();
                }
            }
        }

        private void UseAction()
        {
            currentAction = action.Use(this, Time.deltaTime);
            StartCoroutine(currentAction);
        }
    } 
}
