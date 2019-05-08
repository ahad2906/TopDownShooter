using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum Side
    {
        Top,
        Bottom,
        Left,
        Right
    }
    public Side side;

    public event System.Action<Side> OnEnterDoor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
