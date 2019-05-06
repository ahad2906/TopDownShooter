using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damage;

    void Start() {
     
    }

    void Update() {
        
    }

    void OnTriggerEnter(Collider col) {
        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerHealthManager>().hurtPlayer(damage);
        }
    }
}
