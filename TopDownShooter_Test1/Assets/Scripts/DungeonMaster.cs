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
    private PlayerController player;
    private PoolMan poolMan;
    private Spawner spawner;
    private Room curRoom;
    private int minNbOfRooms = 10, roomCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
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
            GameObject[] enemies = ChooseEnemies();

            if (curRoom != null){
                curRoom.Destroy();
            }
            curRoom = poolMan.ReuseObject(ChooseRoom(), Vector3.zero, Quaternion.identity)
                .GameObject.GetComponent<Room>();
            player.GetComponent<Rigidbody>().position = curRoom.getPlayerSpawn(side);
            curRoom.UnLockDoor(side);
        }
        
    }

    public void OnRoomCleared(){
        //Låser de valgte døre op
        foreach(Door.Side side in ChooseDoors()){
            curRoom.UnLockDoor(side);
        }
    }

    private GameObject ChooseRoom(){
        int pick = Random.Range(1, rooms.Length) - 1;
        return rooms[pick];
    }

    private GameObject[] ChooseEnemies(){
        return null;
    }

    private Door.Side[] ChooseDoors(){
        return null;
    }
}
