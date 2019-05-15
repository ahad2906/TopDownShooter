using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile projectile;
    public float firerate = 100;
    //public AudioClip[] audioArray; kan bruges senere til lyd

    private float nextShotTime;

    void Start()
    {
        //Burde flyttes til hvem der styrer skud (DungeonMaster?)
        PoolMan.Instance.CreatePool(projectile.gameObject, 20);
    }

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + 1f / (firerate / 60f);
            PoolMan.Instance.ReuseObject(projectile.gameObject, muzzle.position, muzzle.rotation);
            //PlaySound(projectileNb);
        }
    }

    /*private void PlaySound(int clip)
    {
        GetComponent<AudioSource>().PlayOneShot(audioArray[clip]);
    } */
}
