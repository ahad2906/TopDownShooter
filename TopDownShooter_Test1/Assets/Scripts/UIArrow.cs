using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIArrow : MonoBehaviour
{
    private Transform target, player;
    public Transform Target
    {
        set
        {
            this.target = value;
            gameObject.SetActive(target != null);
        }
    }

    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        gameObject.SetActive(target != null);
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 dir = target.position - player.position;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + 180;
            transform.localEulerAngles = new Vector3(0, 180, angle);
        }
    }
}
