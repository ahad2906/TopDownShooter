using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Big man in da house
 **/
public class DungeonMaster : MonoBehaviour
{
    public GameObject[] rooms;
    public GameObject[] enemies;
    private Player player;
    private PoolMan poolMan;
    private Spawner spawner;
    private Room curRoom;
    private Wave[] waves;
    private Wave currentWave;
    private int waveNumber, enemiesRemainingToSpawn, enemiesRemainingAlive;
    private float nextSpawnTime;
    private int minNbOfRooms = 10, roomCount;
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();
        poolMan = PoolMan.Instance;
        FillThePool(rooms, 1);
        FillThePool(enemies, 6);
        OnEnterDoor(Door.Side.Bottom);
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime)
        {
            SpawnWave();
        }
    }

    private void FillThePool(GameObject[] prefabs, int size){
        foreach (GameObject prefab in prefabs){
            poolMan.CreatePool(prefab, size);
        }
    }

    public void OnEnterDoor(Door.Side side){
        if (minNbOfRooms > roomCount){
            roomCount++;

            if (curRoom != null){
                curRoom.Destroy();
            }
            curRoom = poolMan.ReuseObject(ChooseRoom(), Vector3.zero, Quaternion.identity)
                .GameObject.GetComponent<Room>();
            player.GetComponent<Rigidbody>().position = curRoom.getPlayerSpawn(side);

            GenerateWaves();
            waveNumber = 0;
            NextWave();
            nextSpawnTime = Time.time + 1f;
        }
        
    }

    private GameObject ChooseRoom(){
        int pick = Random.Range(0, rooms.Length);
        return rooms[pick];
    }

    private GameObject[] ChooseEnemies(){
        //Ikke implementeret
        return null;
    }

    private Door.Side[] ChooseDoors(){
        Door.Side[] sides = new Door.Side[Random.Range(1, 4)];
        List<Door> list = new List<Door>(curRoom.doors);
        for (int i = 0; i < sides.Length; i++)
        {
            int j = Random.Range(0, list.Count);
            sides[i] = list[j].side;
            list.RemoveAt(j);
        }
        return sides;
    }

    public void OnEnemyDeath()
    {
        enemiesRemainingAlive -= 1;

        if (enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }

    public void OnRoomCleared()
    {
        //Låser de valgte døre op
        foreach (Door.Side side in ChooseDoors())
        {
            curRoom.UnLockDoor(side);
        }
    }

    private void GenerateWaves()
    {
        int maxSpawn = curRoom.spawnPoints.Length; //Gemmer hvor mange fjender som kan spawnes på samme tid
        int nbOfEnemies = Random.Range(1, (int)(maxSpawn * Random.Range(1f, 3f))); //Vælger tilfældigt hvor stor puljen skal være
        //Regner ud om der er et skævt antal fjender og retter derefter
        int remainder = nbOfEnemies % maxSpawn;
        int nbOfWaves = (nbOfEnemies - remainder) / maxSpawn;
        if (remainder > 0)
            nbOfWaves++;
        else
            remainder = maxSpawn;
        //instantierer Wave array'et
        waves = new Wave[nbOfWaves];
        //Instantierer hvert enkelt Wave objekt
        for (int i = 0; i < nbOfWaves; i++)
        {
            waves[i] = new Wave();
            waves[i].enemyCount = (i < nbOfWaves - 1) ? maxSpawn : remainder;
            waves[i].spawnInterval = 2f;
        }
    }

    private void SpawnWave()
    {
        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            poolMan.ReuseObject(enemies[0], curRoom.spawnPoints[i].position, Quaternion.identity);
            enemiesRemainingToSpawn--;
        }
        Debug.Log("Remaining: " + enemiesRemainingToSpawn + " Wavenb: " + waveNumber + " Total: " + waves.Length);
    }

    private void NextWave()
    {
        if (waveNumber < waves.Length)
        {
            currentWave = waves[waveNumber];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;
            nextSpawnTime = Time.time + currentWave.spawnInterval;
            waveNumber++;
        }
        else
        {
            //Rummet er cleared
            OnRoomCleared();
        }
    }

    public class Wave
    {
        public int enemyCount;
        public float spawnInterval;

    }
}
