using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
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
    private int enemyCount;
    public event System.Action OnRoomCleared;

    void Start()
    {
        
    }

    public void UnlockDoors()
    {

    }
}
