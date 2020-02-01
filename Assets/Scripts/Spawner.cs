using System.Collections.Generic;
using UnityEngine;
using Utilities;

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

    [SerializeField] float spawnInterval = 0.6f;
    [SerializeField] private float spawnAirEnemiesTime= 15;
    [SerializeField] private float spawnUndergroundEnemiesTime = 30;
    [SerializeField] private float spawnIntervalModifier = 0.025f;
    [SerializeField] private float spawnModificationDelta = 2.5f;
    
    private float spawnTimer;
    private float timer;
    private float spawnModifierTimer;
    private List<Enemy> airEnemyPool;
    private List<Enemy> groundEnemyPool;
    private List<Enemy> undergroundEnemyPool;
    private List<Enemy> spawnedEnemies;
    
    private readonly Vector3 OffscreenPostion = new Vector3(11, 6);

    private void Start()
    {
        spawnedEnemies = new List<Enemy>();
        InitializePools();
    }

    public void Restart()
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; --i)
        {
            spawnedEnemies[i].gameObject.SetActive(false);
            ReturnToPool(spawnedEnemies[i]);
        }
        
        timer = 0;
    }

    private void Update()
    {
        if (GameManager.CurrentGameMode == GameMode.Play)
        {
            spawnTimer += Time.deltaTime;
            timer += Time.deltaTime;
            
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0;
                
                Enemy groundEnemy = GetEnemy(groundEnemyPool, groundEnemyPrefabs);
                SpawnEnemy(groundEnemy, groundSpawnPoints);
                
                if (spawnAirEnemiesTime < timer)
                {
                    Enemy airEnemy = GetEnemy(airEnemyPool, airEnemyPrefabs);
                    SpawnEnemy(airEnemy, airSpawnPoints);
                }

                if (spawnUndergroundEnemiesTime < timer)
                {
                    Enemy underGroundEnemy = GetEnemy(undergroundEnemyPool, undergroundEnemyPrefabs);
                    SpawnEnemy(underGroundEnemy, undergroundSpawnPoints);
                    spawnModifierTimer += Time.deltaTime;
                    
                    if (spawnModifierTimer >= spawnModificationDelta)
                    {
                        spawnModifierTimer = 0;
                        spawnInterval -= spawnIntervalModifier;
                    }
                }
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
        spawnedEnemies.Remove(enemy);
        
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
        spawnedEnemies.Add(enemy);
        
        return enemy;
    }

    private void SpawnEnemy(Enemy enemyToSpawn, Transform[] spawnPoints, bool addOffset = true)
    {
        enemyToSpawn.transform.position = PickSpawnPoint(spawnPoints, addOffset);
        float enemyScale = Random.Range(enemyToSpawn.MinScale, enemyToSpawn.MaxScale);
        enemyToSpawn.transform.localScale = new Vector3(enemyScale, enemyScale, enemyScale);
        
        if (enemyToSpawn.transform.position.x < 0f)
        {
            enemyToSpawn.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            enemyToSpawn.transform.rotation = Quaternion.identity;
        }
        
        enemyToSpawn.gameObject.SetActive(true);
    }
}
