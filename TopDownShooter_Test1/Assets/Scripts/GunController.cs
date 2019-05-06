using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {
    
    public bool isFiring;

    public BulletController bullet;
    public float bulletSpeed;

    public float timeBetweenShot;
    private float shotCounter;

    public Transform firePoint;

    // Update is called once per frame
    void Update() {
        if(isFiring) {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0) {
                shotCounter = timeBetweenShot;
                BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
                newBullet.speed = bulletSpeed;
            } else {
                shotCounter = 0;
            }
        }
    }
}
