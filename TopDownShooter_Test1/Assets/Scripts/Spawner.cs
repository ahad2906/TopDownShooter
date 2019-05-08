using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner: MonoBehaviour {

    public Wave[] waves;
    public EnemyController enemy;

    Wave currentWave;
    int currentWaveNumber;

    int enemiesRemainingToSpawn;
    float nextSpawnTime;

    private void Start() {
        nextWave();
    }

    void Update() {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
            enemiesRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

			EnemyController spawnedEnemy = Instantiate(enemy, transform.position, Quaternion.identity) as EnemyController;
            spawnedEnemy.OnDeath += OnEnemyDeath;
        }
    }

    void OnEnemyDeath() {
        print("Enemy died");
    }

    void nextWave() {
        currentWaveNumber++;
        currentWave = waves[currentWaveNumber - 1];

        enemiesRemainingToSpawn = currentWave.enemyCount;
    }


    [System.Serializable]
    public class Wave {

        public int enemyCount;
        public float spawnInterval;

    }

}
