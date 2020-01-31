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

    private List<Enemy> airEnemyPool;
    private List<Enemy> groundEnemyPool;
    private List<Enemy> undergroundEnemyPool;

    private readonly Vector3 OffscreenPostion = new Vector3(11, 6);

    private void Start()
    {
        InitializePools();
    }

    private void Update()
    {
        if (GameManager.CurrentGameMode == GameManager.GameMode.Play)
        {
            timer += Time.deltaTime;

            if (timer >= spawnTime)
            {
                timer = 0;
                Enemy airEnemy = GetEnemy(airEnemyPool, airEnemyPrefabs);
                Enemy groundEnemy = GetEnemy(groundEnemyPool, groundEnemyPrefabs);
                Enemy underGroundEnemy = GetEnemy(undergroundEnemyPool, undergroundEnemyPrefabs);

                SpawnEnemy(airEnemy, airSpawnPoints);
                SpawnEnemy(groundEnemy, groundSpawnPoints, false);
                SpawnEnemy(underGroundEnemy, undergroundSpawnPoints);
            }
        }
    }

    private void InitializePools()
    {
        InitializePool(ref airEnemyPool, airEnemyPrefabs, airEnemiesPoolSize);
        InitializePool(ref groundEnemyPool, groundEnemyPrefabs, groundEnemiesPoolSize);
        InitializePool(ref undergroundEnemyPool, undergroundEnemyPrefabs, undergroundEnemyPoolSize);
    }

    private void InitializePool(ref List<Enemy> enemiesPool, Enemy[] enemyPrefabs, int poolSize)
    {
        enemiesPool = new List<Enemy>();

        for (int i = 0; i < poolSize; i++)
        {
            Enemy enemy = CreateEnemy(enemyPrefabs);
            AddEnemyToPool(enemiesPool, enemy);
        }
    }

    private Enemy CreateEnemy(Enemy[] enemyPrefabs)
    {
        // Some Random Logic to choose which enemy to spawn now?
        return Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], OffscreenPostion, Quaternion.identity);
    }

    private void AddEnemyToPool(List<Enemy> enemiesPool, Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemy.OnEnemyCollision += ReturnToPool;
        enemiesPool.Add(enemy);
    }

    private void ReturnToPool(Enemy enemy)
    {
        switch (enemy.enemyType)
        {
            case EnemyType.Air:
                airEnemyPool.Add(enemy);
                break;
            case EnemyType.Ground:
                groundEnemyPool.Add(enemy);
                break;
            case EnemyType.Underground:
                undergroundEnemyPool.Add(enemy);
                break;
        }
    }

    private Vector3 PickSpawnPoint(Transform[] possibleSpawnPoints, bool addOffset = true)
    {
        Vector2 spawnOffset = Vector2.zero;

        if (addOffset)
        {
            spawnOffset = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
        }

        return (Vector2) possibleSpawnPoints[Random.Range(0, possibleSpawnPoints.Length)].position + spawnOffset;
    }

    private Enemy GetEnemy(List<Enemy> enemiesPool, Enemy[] enemyPrefabs)
    {
        Enemy enemy;

        if (enemiesPool.Count == 0)
        {
            enemy = CreateEnemy(enemyPrefabs);
            AddEnemyToPool(enemiesPool, enemy);
        }

        int index = Random.Range(0, enemiesPool.Count);
        enemy = enemiesPool[index];
        enemiesPool.RemoveAt(index);

        return enemy;
    }

    private void SpawnEnemy(Enemy enemyToSpawn, Transform[] spawnPoints, bool addOffset = true)
    {
        enemyToSpawn.transform.position = PickSpawnPoint(spawnPoints, addOffset);
        enemyToSpawn.gameObject.SetActive(true);
    }
}
