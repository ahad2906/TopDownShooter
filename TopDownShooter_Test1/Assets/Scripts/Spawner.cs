using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner: MonoBehaviour {

    public Wave[] waves;
    public EnemyController enemy;

    Wave currentWave;
    int currentWaveNumber;

    int enemieRemainingToSpawn;
    float nextSpawnTime;

    private void Start() {
        nextWave();
    }

    void Update() {
        if (enemieRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
            enemieRemainingToSpawn--;
            nextSpawnTime = Time.time + currentWave.spawnInterval;

            EnemyController spawnedEnemy = Instantiate(enemy, Vector3.zero, Quaternion.identity) as EnemyController;

        }
    }

    void nextWave() {
        currentWaveNumber++;
        currentWave = waves[currentWaveNumber - 1];

        enemieRemainingToSpawn = currentWave.enemyCount;
    }


    [System.Serializable]
    public class Wave {

        public int enemyCount;
        public float spawnInterval;

    }

}
