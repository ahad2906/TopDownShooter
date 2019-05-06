using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float speed;

    public float lifeTime;

    public int damageToGive;

	private bool hasCollided = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        // transform.translate = fortæller objektet at det skal rykke til nyt punkt
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }

    public void setSpeed(float newSpeed) {
        speed = newSpeed;
    }


    void OnCollisionEnter(Collision col) {
		if (!hasCollided) {
			hasCollided = true;

			IDamageable damageable = col.gameObject.GetComponent<IDamageable> ();

			if (damageable != null) {
				damageable.Damage (damageToGive);
			}

			Destroy (gameObject);
		}

        /*if (col.gameObject.tag == "Enemy") {
            col.gameObject.GetComponent<EnemyHealthManager>().hurtEnemy(damageToGive);
            Destroy(gameObject);
        } else if (col.gameObject.tag == "Obstacle") {
            Destroy(gameObject);
        }*/
    }
}
