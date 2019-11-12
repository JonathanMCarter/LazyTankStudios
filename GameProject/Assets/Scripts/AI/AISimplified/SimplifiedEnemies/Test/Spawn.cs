using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class Spawn : Ability
    {
        public GameObject Monster;
        public float Radius;
        public int maxSpawns;

        private List<GameObject> spawnedObjects = new List<GameObject>();

        public override void Interrupt()
        {
        }

        public override void Use()
        {
            if(spawnedObjects.Count < maxSpawns)
                spawnedObjects.Add(Instantiate(Monster, new Vector2(Random.Range(transform.position.x - Radius, transform.position.x + Radius), Random.Range(transform.position.y - Radius, transform.position.y + Radius)), Quaternion.identity));
        }
    } 
}
