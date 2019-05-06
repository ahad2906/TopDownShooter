using UnityEngine;

public class PlayerHealthManager : MonoBehaviour {

    public int startingHealth;
    public int currHealth;

    // Start is called before the first frame update
    void Start() {
        currHealth = startingHealth;
    }

    // Update is called once per frame
    void Update() {
        if (currHealth <=  0) {
            gameObject.SetActive(false);
        }
    }

    public void hurtPlayer(int damageAmount) {
        currHealth -= damageAmount;
    }

}
