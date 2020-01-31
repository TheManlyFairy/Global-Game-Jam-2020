using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform[] airSpawnPoints;
    [SerializeField] private Transform[] groundSpawnPoints;
    [SerializeField] private Transform[] undergroundSpawnPoints;

    [SerializeField] private Enemy[] airEnemyPrefabs;
    [SerializeField] private Enemy[] groundEnemyPrefabs;
    [SerializeField] private Enemy[] undergroundEnemyPrefabs;

    [SerializeField] int airEnemiesPoolSize;
    [SerializeField] int groundEnemiesPoolSize;
    [SerializeField] int undergroundEnemyPoolSize;

    [SerializeField] float spawnTime;
    float timer;
    
    private Queue<Enemy> airEnemyPool;
    private Queue<Enemy> groundEnemyPool;
    private Queue<Enemy> undergroundEnemyPool;

    private readonly Vector3 OffscreenPostion = new Vector3(11,6);
    
    private void Start()
    {
        InitializePools();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (timer >= spawnTime)
        {
            timer = 0;
            SpawnEnemy(airEnemyPool, airEnemyPrefabs, airSpawnPoints);
            SpawnEnemy(groundEnemyPool, groundEnemyPrefabs, groundSpawnPoints);
            SpawnEnemy(undergroundEnemyPool, undergroundEnemyPrefabs, undergroundSpawnPoints);
        }
    }

    private void InitializePools()
    {
        InitializePool(ref airEnemyPool, airEnemyPrefabs, airEnemiesPoolSize);
        InitializePool(ref groundEnemyPool, groundEnemyPrefabs, groundEnemiesPoolSize);
        InitializePool(ref undergroundEnemyPool, undergroundEnemyPrefabs, undergroundEnemyPoolSize);
    }
    
    private void InitializePool(ref Queue<Enemy> enemiesPool, Enemy[] enemyPrefabs, int poolSize)
    {
        enemiesPool = new Queue<Enemy>();
        
        for (int i = 0; i < poolSize; i++)
        {
            // Spawn all types on  😘
            Enemy enemy = CreateEnemy(enemyPrefabs);
            AddEnemyToPool(enemiesPool, enemy);
        }
    }

    private Enemy CreateEnemy(Enemy[] enemyPrefabs)
    {
        // Some Random Logic to choose which enemy to spawn now?
        return Instantiate(enemyPrefabs[0], OffscreenPostion, Quaternion.identity);
    }

    private void AddEnemyToPool(Queue<Enemy> enemiesPool, Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.OnEnemyCollision += ReturnToPool;
        enemiesPool.Enqueue(enemy);
    }

    private void ReturnToPool(Enemy enemy)
    {
        switch (enemy.enemyType)
        {
            case EnemyType.Air: 
                airEnemyPool.Enqueue(enemy);
                break;
            case EnemyType.Ground : 
                groundEnemyPool.Enqueue(enemy);
                break;
            case EnemyType.Underground:
                undergroundEnemyPool.Enqueue(enemy);
                break;
        }
    }

    private Vector3 PickSpawnPoint(Transform[] possibleSpawnPoints)
    {
        Vector2 spawnOffset = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));

        return (Vector2) possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Length)].position + spawnOffset;
    }

    private void SpawnEnemy(Queue<Enemy> enemiesPool,Enemy[] enemyPrefabs, Transform[] spawnPoints)
    {
        if (enemiesPool.Count == 0)
        {
            Enemy enemy = CreateEnemy(enemyPrefabs);
            AddEnemyToPool(enemiesPool, enemy);
        }

        Enemy dequeuedEnemy = enemiesPool.Dequeue();
        dequeuedEnemy.transform.position = PickSpawnPoint(spawnPoints);
        dequeuedEnemy.gameObject.SetActive(true);
    }
}
