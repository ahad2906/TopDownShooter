using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile projectile;
    public float fireRate = 100;
    public AudioClip[] audioArray;

    float nextShotTime;

    void Start()
    {
        //Burde flyttes til hvem der styrer skud (DungeonMaster?)
        PoolMan.Instance.CreatePool(projectile.gameObject, 20);
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + 1f / (fireRate / 60f);
            PoolMan.Instance.ReuseObject(projectile.gameObject, muzzle.position, muzzle.rotation);
            //PlaySound(projectileNb);
        }
    }

    void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioArray[clip]);
    }
}
