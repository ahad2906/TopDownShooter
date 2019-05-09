using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Big man in da house
 **/
public class DungeonMaster : MonoBehaviour
{
    private Room curRoom, prevRoom;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnterDoor(Door.Side side){
        prevRoom = curRoom;
        curRoom = ChooseRoom();
    }

    public void OnRoomCleared(){
        //Låser de valgte døre op
        foreach(Door.Side side in ChooseDoors()){
            curRoom.UnLockDoor(side);
        }
    }

    private Room ChooseRoom(){
        return null;
    }

    private EnemyController[] ChooseEnemies(){
        return null;
    }

    private Door.Side[] ChooseDoors(){
        return null;
    }
}
