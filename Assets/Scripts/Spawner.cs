using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    Transform[] airSpawnPoints;
    [SerializeField]
    Transform[] groundSpawnPoints;
    [SerializeField]
    Transform[] undergroundSpawnPoints;

    [SerializeField]
    Enemy[] airEnemyPrefabs;
    [SerializeField]
    Enemy[] groundEnemyPrefabs;
    [SerializeField]
    Enemy[] undergroundEnemyPrefabs;

    Queue<Enemy> airEnemyPool;
    Queue<Enemy> groundEnemyPool;
    Queue<Enemy> undergroundEnemyPool;

    [SerializeField]
    int airEnemiesPoolSize;
    [SerializeField]
    int groundEnemiesPoolSize;
    [SerializeField]
    int undergroundEnemyPoolSize;

    [SerializeField]
    float spawnTime;
    float timer;
    void Start()
    {
        InitializeAirPool();
        
        Enemy.onEnemyCollision += QueueAirEnemy;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnTime)
        {
            timer = 0;
            SpawnAirEnemy();
        }
    }

    private void InitializePools()
    {
        InitializePool(airEnemyPool, airEnemyPrefabs, airEnemiesPoolSize);
        InitializePool(groundEnemyPool, groundEnemyPrefabs, groundEnemiesPoolSize);
        InitializePool(undergroundEnemyPool, undergroundEnemyPrefabs, undergroundEnemyPoolSize);
    }

    private void InitializePool(Queue<Enemy> enemiesPool, Enemy[] prefab, int poolSize)
    {
        airEnemyPool = new Queue<Enemy>();
        Enemy instantiatedEnemy;
        for (int i = 0; i < airEnemiesPoolSize; i++)
        {
            instantiatedEnemy = Instantiate(airEnemyPrefabs[0], new Vector3(11, 6, 0), Quaternion.identity);
            instantiatedEnemy.gameObject.SetActive(false);
            airEnemyPool.Enqueue(instantiatedEnemy);
        }
    }
    
    void InitializeAirPool()
    {
        airEnemyPool = new Queue<Enemy>();
        Enemy instantiatedEnemy;
        for (int i = 0; i < airEnemiesPoolSize; i++)
        {
            instantiatedEnemy = Instantiate(airEnemyPrefabs[0], new Vector3(11, 6, 0), Quaternion.identity);
            instantiatedEnemy.gameObject.SetActive(false);
            airEnemyPool.Enqueue(instantiatedEnemy);
        }
    }

    void SpawnAirEnemy()
    {
        if (airEnemyPool.Count > 0)
        {
            Vector2 spawnOffset = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            Enemy dequeuedEnemy = airEnemyPool.Dequeue();
            dequeuedEnemy.transform.position = (Vector2)airSpawnPoints[Random.Range(0, 3)].position + spawnOffset;
            dequeuedEnemy.gameObject.SetActive(true);
        }
        else
        {
            AddEnemyToPool();
        }
    }

    void AddEnemyToPool()
    {
        Enemy instantiatedEnemy;
        instantiatedEnemy = Instantiate(airEnemyPrefabs[0], new Vector3(11, 6, 0), Quaternion.identity);
        instantiatedEnemy.gameObject.SetActive(false);
        airEnemyPool.Enqueue(instantiatedEnemy);
    }

    void QueueAirEnemy(Enemy enemyToQueue)
    {
        airEnemyPool.Enqueue(enemyToQueue);
    }
}
