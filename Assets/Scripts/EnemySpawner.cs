using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public Rigidbody2D playerRigidbody;
    public GameObject[] enemyPrefabs;
    public GameObject bossPrefab;

    public int initialSpawnCount = 20;

    public float spawnDelay = 0.1f; //wait time between each spawn
    public float secondsUntilNextWave = 20f;
    public int spawnIncreaseAmount = 10;
    public int bossSpawnCount = 5;
    public float rareSpawnChance = 0.3f;
    public float spawnRadius = 10f;

    private int currentSpawnCount;
    private int waveCounter;
    private int bossSpawnCounter;
    private Camera mainCamera;

    public static event Action OnNewWave;
    private void Start() {
        currentSpawnCount = initialSpawnCount;
        mainCamera = Camera.main;
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies() {
        while (true) {
            Debug.Log(waveCounter);
            //Spawn the enemies
            OnNewWave?.Invoke();
            for (int i = 0; i < currentSpawnCount; i++) {
                GameObject enemyPrefab = GetRandomEnemyPrefab();
                Vector3 spawnPosition = GetRandomSpawnPosition();
                
                GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
                Enemy enemyComponent = enemy.GetComponent<Enemy>();
                enemyComponent.SetPlayerRigidBody(playerRigidbody);
                enemyComponent.IncreaseMaxHealth((int)MathF.Floor(Mathf.Pow((float)waveCounter, 4f)));
                enemyComponent.IncreaseContactDamage((int)MathF.Floor(Mathf.Pow((float)waveCounter, 4f)));

                yield return new WaitForSeconds(spawnDelay);
            }

            if(currentSpawnCount < 200) {
                currentSpawnCount += spawnIncreaseAmount;
            }
            waveCounter++;
            if(rareSpawnChance > 0.15f) {
                rareSpawnChance -= 0.1f;
            }
            
            //Spawn boss every 5 rounds
            if (waveCounter % bossSpawnCount == 0) {
                if (waveCounter < 25) {
                    secondsUntilNextWave += 1.5f;
                } else if(waveCounter < 100){
                    secondsUntilNextWave += 2f;
                }
                secondsUntilNextWave += 2;
                for (int i = 0; i < bossSpawnCounter + 1; i++) {
                    Vector3 spawnPosition = GetRandomSpawnPosition();
                    GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
                    Enemy bossEnemyComponent = boss.GetComponent<Enemy>();
                    bossEnemyComponent.SetPlayerRigidBody(playerRigidbody);
                    bossEnemyComponent.IncreaseMaxHealth((int)MathF.Floor(Mathf.Pow((float)waveCounter, 4f)));
                    bossEnemyComponent.IncreaseContactDamage((int)MathF.Floor(Mathf.Pow((float)waveCounter, 4f)));
                    yield return new WaitForSeconds(spawnDelay);
                }
                bossSpawnCounter++;
            }

    
            yield return new WaitForSeconds(secondsUntilNextWave);
        }
    }

    private GameObject GetRandomEnemyPrefab() {
        float randomValue = UnityEngine.Random.value;
        if (randomValue < rareSpawnChance) {
            return enemyPrefabs[UnityEngine.Random.Range(2, 4)];
        } else {
            return enemyPrefabs[UnityEngine.Random.Range(0, 2)];
        }
    }

    private Vector3 GetRandomSpawnPosition() {
        float x = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
        float y = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
        Vector3 spawnPosition = new Vector3(x, y, 0f);
        while (IsPositionWithinCameraView(spawnPosition)) {
            x = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
            y = UnityEngine.Random.Range(-spawnRadius, spawnRadius);
            spawnPosition = new Vector3(x, y, 0f);
        }
        return spawnPosition;
    }

    private bool IsPositionWithinCameraView(Vector3 position) {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(position);
        if (screenPoint.x > 0f && screenPoint.x < 1f && screenPoint.y > 0f && screenPoint.y < 1f) {
            return true;
        } else {
            return false;
        }
    }

    public int GetCurrentWave() {
        return waveCounter;
    }
}