﻿using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    public int startingHealth;
    public int currHealth;

    public float flashLength;
    private float flashCounter;

    // variabel til at gemme objektets nuværende farve
    private Renderer rend;
    private Color storedColor;

    bool isDamaged = true;
    bool isDead = true;

    // Start is called before the first frame update
    void Start() {
        currHealth = startingHealth;
        rend = GetComponent<Renderer>();
        storedColor = rend.material.GetColor("_Color");

    }

    // Update is called once per frame
    void Update() {

        if (currHealth <=  0) {
            gameObject.SetActive(false);
            isDead = true;
            onDeath();
        }

        if(flashCounter > 0) {
            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0) {
                rend.material.SetColor("_Color", storedColor);
            }
        }

    }

    // Hvad der sker når spilleren er død
    void onDeath() {

    }

    public void hurtPlayer(int damageAmount) {
        isDamaged = true;
        currHealth -= damageAmount;

        flashCounter = flashLength;
        rend.material.SetColor("_Color", Color.white);
    }

}
