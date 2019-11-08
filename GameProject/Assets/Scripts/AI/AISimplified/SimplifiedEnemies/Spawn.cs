using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Final
{
    public class Spawn : Ability
    {
        [SerializeField] GameObject SpawnObject;
        [SerializeField] private float radius;
        [SerializeField] private int maxSpawns;
        private List<GameObject> gameObjects = new List<GameObject>();

        public override IEnumerator Use()
        {
            if(gameObjects.Count < maxSpawns )
                gameObjects.Add(Instantiate(SpawnObject, new Vector2(transform.position.x + Random.Range(-radius, radius), transform.position.y + Random.Range(-radius, radius)), Quaternion.identity));
            yield return null;
        }       
    } 
}
