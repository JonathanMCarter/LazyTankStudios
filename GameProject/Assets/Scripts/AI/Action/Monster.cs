using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Monster : MonoBehaviour
    {
        public GameObject target;

        public Ability ability;
        private IEnumerator currentAction;
        private Chase chase;

        string test = "if(true)Debug.Log('nice');";

        void Start()
        {
            chase = (Chase)ability;
            chase.Target = target.transform;
            chase.Init();
            UseAction();
            () => { test };
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!currentAction.MoveNext())
                {
                    UseAction();
                }
            }
        }
        private void UseAction()
        {
            currentAction = chase.Use(this, Time.deltaTime);
            StartCoroutine(currentAction);
        }
    }
}
