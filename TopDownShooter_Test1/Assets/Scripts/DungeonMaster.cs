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
    private int minNbOfRooms = 10, roomCount = 0;
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
            curRoom.UnLockDoor(side);

            for(int i = 0; i < curRoom.spawnPoints.Length; i++)
            {
                poolMan.ReuseObject(enemies[0], curRoom.spawnPoints[i].position, Quaternion.identity);
            }
        }
        
    }

    public void OnRoomCleared(){
        //Låser de valgte døre op
        foreach(Door.Side side in ChooseDoors()){
            curRoom.UnLockDoor(side);
        }
    }

    private GameObject ChooseRoom(){
        int pick = Random.Range(0, rooms.Length);
        return rooms[pick];
    }

    private GameObject[] ChooseEnemies(){
        //TODO: fix it, write proper algorithm that chooses enemy types based on progression
        List<GameObject> enemyPool = new List<GameObject>(enemies);
        List<GameObject> pickedEnemies = new List<GameObject>();
        int max = (int)Random.value * 10 + 1;
        for (int i = 0; i < max; i++)
        {
            if (enemyPool.Count <= 0)
                break;

            foreach(GameObject enemy in enemies)
            {
                if (Random.value > .5f || pickedEnemies.Count == 0)
                {
                    pickedEnemies.Add(enemy);
                    enemyPool.Remove(enemy);
                    break;
                }
            }
        }
        Debug.Log(pickedEnemies.Count);
        return pickedEnemies.ToArray();
    }

    private Door.Side[] ChooseDoors(){
        return null;
    }
}
