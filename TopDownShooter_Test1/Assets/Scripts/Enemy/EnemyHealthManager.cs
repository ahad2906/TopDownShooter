using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour, IDamageable {

    public int health;
    public int currHealth;


    // Start is called before the first frame update
    void Start() {
        currHealth = health;
    }

    // Update is called once per frame
    void Update() {
        
    }

	public void Damage(int amount) {
		currHealth -= amount;
		if (currHealth <= 0) {
			Destroy(gameObject);
		}
	}

    /*public void hurtEnemy(int damage) {
        currHealth -= damage;
    }*/
}
