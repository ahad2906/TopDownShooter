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
    private Room curRoom;
    private Wave[] waves;
    private Wave currentWave;
    private int waveNumber, enemiesRemainingToSpawn, enemiesRemainingAlive;
    private float nextSpawnTime;
    private int minNbOfRooms = 10, roomCount;
    public event System.Action<Transform[]> OnRoomCleared;
    public event System.Action OnNewRoom;
    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        player = FindObjectOfType<Player>();
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
        //Kald vores listeners, hvis nogle
        if (OnNewRoom != null)
        {
            OnNewRoom();
        }
        //Checker at vi ikke har passeret det min antal rum
        //Ideen her er så at vælge et bossrum hvis det ikke er tilfældet
        if (minNbOfRooms > roomCount){
            roomCount++;

            if (curRoom != null){
                curRoom.Destroy();
            }
            //Vælger et tilfældigt rum og tager det ud af puljen
            curRoom = poolMan.ReuseObject(ChooseRoom(), Vector3.zero, Quaternion.identity)
                .GameObject.GetComponent<Room>();
            //Placer spilleren forand modsatte dør han gik ud af
            player.GetComponent<Rigidbody>().position = curRoom.getPlayerSpawn(side);

            //Genrerer de "bølger" og antallet af fjender som der kommer i rummet
            GenerateWaves();
            waveNumber = 0;
            //Starter den næste "bølge" af fjender
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

    private Door[] ChooseDoors(){
        int doorsToRemove = Random.Range(1, 4);
        List<Door> list = new List<Door>(curRoom.doors);
        for (int i = 0; i < doorsToRemove; i++)
        {
            list.RemoveAt(Random.Range(0, list.Count));
        }
        return list.ToArray();
    }

    public void OnEnemyDeath()
    {
        enemiesRemainingAlive -= 1;

        if (enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }

    private void RoomCleared()
    {
        List<Transform> transforms = new List<Transform>();

        //Låser de valgte døre op og tilføjer deres transform til listen
        foreach (Door door in ChooseDoors())
        {
            curRoom.UnLockDoor(door);
            transforms.Add(door.transform);
        }

        //Kalder listeners hvis der findes nogle
        if (OnRoomCleared != null)
            OnRoomCleared(transforms.ToArray());
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
            RoomCleared();
        }
    }

    public class Wave
    {
        public int enemyCount;
        public float spawnInterval;

    }
}
