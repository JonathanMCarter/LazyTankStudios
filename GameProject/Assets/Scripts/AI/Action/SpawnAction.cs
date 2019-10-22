using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Spawn : Ability
    {
        private Test.Action action;
        public GameObject SpawnObject;
        public float Radius;

        public void Init(GameObject spawnObject, float radius)
        {
            SpawnObject = spawnObject;
            Radius = radius;
            action = new Action();
            action.ActionDelegate = SpawnAction;
        }

        public override IEnumerator Use(MonoBehaviour mb, float deltaTime)
        {
            yield return action.Use(mb, deltaTime);
        }

        public void SpawnAction(MonoBehaviour mb, float deltaTime) => Instantiate(SpawnObject, new Vector2(Random.Range(-Radius, Radius), Random.Range(-Radius, Radius)), Quaternion.identity);
    } 
}
