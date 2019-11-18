using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private float radius;
    [SerializeField] private int maxSpawn;
    private float time;
    private Vector2 position;
    private Stack<GameObject> spawnedObjects;
    void Start()
    {
        time = spawnCooldown;
        spawnedObjects = new Stack<GameObject>();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time < spawnCooldown) return;
        time = 0f;
        if (spawnedObjects.Count == maxSpawn) return;
        position = new Vector2(transform.position.x + Random.Range(-radius, radius), transform.position.y + Random.Range(-radius, radius));
        spawnedObjects.Push(Instantiate(prefab, position, Quaternion.identity));
    }
}
