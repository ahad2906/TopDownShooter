using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject uiArrow;
    private Queue<UIArrow> arrows;
    private bool trackTarget;

    // Start is called before the first frame update
    void Start()
    {
        DungeonMaster dm = FindObjectOfType<DungeonMaster>();
        if (dm != null)
        {
            dm.OnNewRoom += OnNewRoom;
            dm.OnRoomCleared += OnRoomCLeared;
        }

        arrows = new Queue<UIArrow>();
        for (int i = 0; i < 4; i++)
        {
            UIArrow arrow = Instantiate(uiArrow, transform).GetComponent<UIArrow>();
            arrows.Enqueue(arrow);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trackTarget)
        {
            
        }
    }

    public void OnNewRoom()
    {
        foreach(UIArrow arrow in arrows)
        {
            arrow.Target = null;
        }
    }

    public void OnRoomCLeared(Transform[] doors)
    {
        foreach(Transform door in doors)
        {
            UIArrow arrow = arrows.Dequeue();
            arrow.Target = door;
            arrows.Enqueue(arrow);
        }
    }
}
