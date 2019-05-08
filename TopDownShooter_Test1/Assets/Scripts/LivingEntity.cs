using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, IDamageable {

    public int startingHealth;
    protected int health;

    bool isDamaged;
    protected bool isDead;

    public float flashLength;
    private float flashCounter;

    // variabel til at gemme objektets nuværende farve
    private Renderer rend;
    private Color storedColor;

    public event System.Action OnDeath;

    protected virtual void Start() {
        health = startingHealth;
        rend = GetComponent<Renderer>();
        storedColor = rend.material.GetColor("_Color");
    }

    // Update is called once per frame
    public void Update() {
        if (flashCounter > 0) {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)        {
                rend.material.SetColor("_Color", storedColor);
            }
        }

        if (health <= 0) {
            gameObject.SetActive(false);
            Die();
        }
    }

    public void Damage(int amount) {
        isDamaged = true;
        health -= amount;

        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.white);

        if (health <= 0) {
            Die();
        }
    }

    public void Die() {
        isDead = true;
        if (OnDeath != null) {
            OnDeath();
        }
        gameObject.SetActive(false);
    }

}





