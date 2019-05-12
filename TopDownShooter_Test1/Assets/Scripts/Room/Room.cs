using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour, IPoolable
{
    public enum Size
    {
        Small,
        Medium,
        Big
    }

    public enum Type
    {
        Start,
        Normal,
        Shop,
        Holly,
        Boss
    }

    public Transform[] spawnPoints;
    public Door[] doors;

    void Start()
    {
        
    }

    public void Initialize(int nbofdoors){

    }

    public void UnLockDoor(Door uDoor){
        foreach (Door door in doors){
            if (door.side == uDoor.side) {
                door.state = Door.State.Unlocked;
            }
        }
    }

    private void LockDoors()
    {
		foreach (Door door in doors)
		{
			door.state = Door.State.Locked;
		}
    }

    public Vector3 getPlayerSpawn(Door.Side side){
        Vector3 v = Vector3.zero;
        //Finder den modsate dør
        System.Array values = System.Enum.GetValues(typeof(Door.Side));
        int opposite = ((int)side + 2) % values.Length;
        side = (Door.Side)values.GetValue(opposite);

        foreach (Door door in doors){
            if (door.side == side){
                v = door.GetPlayerSpawn();
            }
        }

        return v;
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void OnCreate()
    {
        DungeonMaster dungeonMaster = FindObjectOfType<DungeonMaster>();

        if (dungeonMaster != null){
            foreach (Door door in doors){
                door.OnEnterDoor += dungeonMaster.OnEnterDoor;
                door.state = Door.State.Locked;
            }
        }
    }

    public void OnReuse()
    {
        LockDoors();
    }
}
