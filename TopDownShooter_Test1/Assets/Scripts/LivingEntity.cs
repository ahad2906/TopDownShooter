using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LivingEntity : MonoBehaviour, IDamageable {

    public int startingHealth;
    protected int health;
    protected float healthPercent;

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
    protected virtual void Update() {
        if (flashCounter > 0) {
            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)        {
                rend.material.SetColor("_Color", storedColor);
            }
        }
    }

    public virtual void Damage(int amount) {
        isDamaged = true;
        health -= amount;
        healthPercent = (float)health / startingHealth;
        Debug.Log("Health at " + healthPercent + "%");

        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.white);

        if (health <= 0) {
            Die();
        }
    }

    protected virtual void Die() {
        isDead = true;
        if (OnDeath != null) {
            OnDeath();
        }
    }

}





