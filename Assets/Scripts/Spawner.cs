using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject zombiePrefab;
    public GameObject healthPackPrefab;
    public GameObject ammoPackPrefab;

    [Header("Spawn Points")]
    public Transform[] zombieSpawnPoints;
    public Transform[] healthSpawnPoints;
    public Transform[] ammoSpawnPoints;

    [Header("Wave Settings")]
    public float timeBetweenWaves = 2f;
    public float spawnDelayBetweenZombies = 0.5f;

    private bool isSpawningWave = false;
    private bool waitingForNextWave = false;
    private int zombiesToSpawnThisWave = 0;
    private int zombiesSpawnedThisWave = 0;

    [Header("Extra Spawns")]
    public float healthSpawnInterval = 8f;
    public float ammoSpawnInterval = 8f;

    private float healthTimer;
    private float ammoTimer;

    private void Start()
    {
        StartCurrentWave();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameEnded())
            return;

        healthTimer += Time.deltaTime;
        ammoTimer += Time.deltaTime;

        if (healthTimer >= healthSpawnInterval)
        {
            healthTimer = 0f;
            SpawnHealthPack();
        }

        if (ammoTimer >= ammoSpawnInterval)
        {
            ammoTimer = 0f;
            SpawnAmmoPack();
        }

        CheckWaveComplete();
    }

    private void StartCurrentWave()
    {
        isSpawningWave = true;
        waitingForNextWave = false;
        zombiesSpawnedThisWave = 0;

        int wave = GameManager.Instance.currentWave;

        // ✅ Set zombie count
        if (wave == 1)
            zombiesToSpawnThisWave = 5;
        else if (wave == 2)
            zombiesToSpawnThisWave = 10;
        else
            zombiesToSpawnThisWave = 15;

        // ✅ Set spawn speed + difficulty
        if (wave == 1)
        {
            spawnDelayBetweenZombies = 0.5f;
            GameManager.Instance.zombieSpeedMultiplier = 1f;
        }
        else if (wave == 2)
        {
            spawnDelayBetweenZombies = 0.4f;
            GameManager.Instance.zombieSpeedMultiplier = 1.2f;
        }
        else
        {
            spawnDelayBetweenZombies = 0.3f;
            GameManager.Instance.zombieSpeedMultiplier = 1.4f;
        }

        GameManager.Instance.UpdateWaveUI();

        StartCoroutine(SpawnWaveRoutine());
    }

    private IEnumerator SpawnWaveRoutine()
    {
        while (zombiesSpawnedThisWave < zombiesToSpawnThisWave)
        {
            SpawnZombie();
            zombiesSpawnedThisWave++;
            yield return new WaitForSeconds(spawnDelayBetweenZombies);
        }

        isSpawningWave = false;
    }

    private void CheckWaveComplete()
    {
        if (isSpawningWave) return;
        if (waitingForNextWave) return;

        int alive = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (alive > 0) return;

        if (GameManager.Instance.currentWave >= GameManager.Instance.finalWave)
        {
            GameManager.Instance.LevelClear();
            return;
        }

        waitingForNextWave = true;
        StartCoroutine(StartNextWaveAfterDelay());
    }

    private IEnumerator StartNextWaveAfterDelay()
    {
        yield return new WaitForSeconds(timeBetweenWaves);

        GameManager.Instance.currentWave++;
        StartCurrentWave();
    }

    private void SpawnZombie()
    {
        Transform point = zombieSpawnPoints[Random.Range(0, zombieSpawnPoints.Length)];
        Instantiate(zombiePrefab, point.position, Quaternion.identity);
    }

    private void SpawnHealthPack()
    {
        Transform point = healthSpawnPoints[Random.Range(0, healthSpawnPoints.Length)];
        Instantiate(healthPackPrefab, point.position, Quaternion.identity);
    }

    private void SpawnAmmoPack()
    {
        Transform point = ammoSpawnPoints[Random.Range(0, ammoSpawnPoints.Length)];
        Instantiate(ammoPackPrefab, point.position, Quaternion.identity);
    }
}