using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Monster : MonoBehaviour
    {
        public GameObject target;

        private MoveAction action;
        private IEnumerator currentAction;

        void Start()
        {
            action = ScriptableObject.CreateInstance<MoveAction>();
            action.Init(target.transform.position, 2f);
        }

        void Update() => UseAction();

        private void UseAction()
        {
            action.target = target.transform.position;
            currentAction = action.Use(this, Time.deltaTime);
            StartCoroutine(currentAction);
        }
    } 
}
//if (Input.GetKeyDown(KeyCode.Space))
//{
//    if(!currentAction.MoveNext())
//    {
//        Debug.Log("Use");
//        UseAction();
//    }
//}