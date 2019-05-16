using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpsAvailable { shootSpeed, speedBoost, healthBoost}

//Test af power-ups, burde i princippet bruges med PoolMan og implementere IPoolable
//Samt deles op i klasser el lign.
public class PowerUp : MonoBehaviour
{

    public PowerUpsAvailable powerUpType;

    public GameObject pickUpEffect;
    
    public float shootSpeedMultiplier;
    public float speedBoostultiplier;
    public int healthBoostMultiplier;

    public float duration; 


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(PickUp(other));
        }
    }


    IEnumerator PickUp(Collider player)
    {
        Player playerStats = player.GetComponent<Player>();
        Gun gunStats = playerStats.Gun;
        transform.parent = player.transform;

        switch (powerUpType)
        {
            case PowerUpsAvailable.shootSpeed:
                gunStats.firerate *= shootSpeedMultiplier;

                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                yield return new WaitForSeconds(duration);

                gunStats.firerate /= shootSpeedMultiplier;

                Destroy(gameObject);
                break;

            case PowerUpsAvailable.speedBoost:
                playerStats.moveSpeed *= speedBoostultiplier;

                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                yield return new WaitForSeconds(duration);

                playerStats.moveSpeed /= speedBoostultiplier;

                Destroy(gameObject);
                break;

            case PowerUpsAvailable.healthBoost:
                playerStats.startingHealth += healthBoostMultiplier;

                GetComponent<MeshRenderer>().enabled = false;
                GetComponent<Collider>().enabled = false;

                yield return new WaitForSeconds(duration);

                Destroy(gameObject);
                break;

            default:
                Debug.Log("Powerup Type does not work, brah!!");
                break;
        }

    }

}
