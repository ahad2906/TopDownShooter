using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private const float PLAYER_DIST = 2f;
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

    public Vector3 GetPlayerSpawn(){
        return transform.position + transform.forward * PLAYER_DIST;  
    }
    private void UpdateColor(){
        GetComponent<Renderer>().material
        .SetColor("_Color", (_state == State.Unlocked)? Color.blue : Color.red);
    }
}
