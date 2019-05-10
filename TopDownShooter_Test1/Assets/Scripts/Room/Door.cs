using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private const float PLAYER_DIST = 1f;
    private readonly Color COL_LOCKED = Color.red, COL_UNLOCKED = Color.blue;
	public enum State
	{
		Locked,
		Unlocked
	}
	private State _state;
    public State state {
        get { return _state;}
        set {
            _state = value;
            UpdateColor();
        }
    }

    public enum Side
    {
        Top,
        Left,
        Bottom,
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

    public Vector3 GetPlayerSpawn(){
        return transform.position + transform.forward * PLAYER_DIST;  
    }
    private void UpdateColor(){
        GetComponent<Renderer>().material
        .SetColor("_Color", (_state == State.Unlocked)? COL_UNLOCKED : COL_LOCKED);
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (OnEnterDoor != null && 
        other.GetComponent<PlayerController>() != null)
        {
            OnEnterDoor(side);
        }
    }
}
