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
    private Room curRoom, prevRoom;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        poolMan = PoolMan.Instance;
        FillThePool(rooms, 3);
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
        //prevRoom = curRoom;
        EnemyController[] enemies = ChooseEnemies();
        curRoom = poolMan.ReuseObject(ChooseRoom(), Vector3.zero, Quaternion.identity)
        .GameObject.GetComponent<Room>();
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

    private EnemyController[] ChooseEnemies(){
        return null;
    }

    private Door.Side[] ChooseDoors(){
        return null;
    }
}
