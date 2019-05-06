using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {

    public int damage;

    public bool enter = true;


    void Start() {
     
    }

    void Update() {
        
    }

    void OnTriggerEnter(Collider col) {

		IDamageable damageable = col.gameObject.GetComponent<IDamageable> ();

		if (damageable != null && col.gameObject.tag == "Player") {
			damageable.Damage (damage);
		}

        /*if(enter && col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerHealthManager>().hurtPlayer(damage);
        }

        if (col.gameObject.tag == "Player") {
            col.gameObject.GetComponent<PlayerHealthManager>().hurtPlayer(damage);
        }*/
    }
}
