using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    public int startingHealth;
    public int currHealth;

    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    bool isDamaged = true;
    bool isDead = true;

    // Start is called before the first frame update
    void Start() {
        currHealth = startingHealth;
    }

    // Update is called once per frame
    void Update() {

        // to-do: farve indikering når player tager damage
        if(isDamaged) {
           
        }

        if (currHealth <=  0) {
            gameObject.SetActive(false);
            isDead = true;
            onDeath();
        }
    }

    // Hvad der sker når spilleren er død
    void onDeath() {

    }

    public void hurtPlayer(int damageAmount) {
        isDamaged = true;
        currHealth -= damageAmount;
    }

}
