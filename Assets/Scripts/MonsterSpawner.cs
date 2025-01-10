using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Monster[] prefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField]  Transform[] spawnPoints;
    private GameManager gameManager;
    
    float nextSpawn;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (Time.time >= nextSpawn)
        {
            nextSpawn = Time.time + spawnInterval;
            SpawnMonster();
        }
    }
    
    private void SpawnMonster()
    {
        var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var spawnMonster = prefab[Random.Range(0, prefab.Length)];
        
        var monster = Instantiate(spawnMonster, spawnPoint.position, spawnPoint.rotation);
        monster.Init(gameManager);
    }
}


