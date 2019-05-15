using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IPoolable
{
    public LayerMask collisionMask;
    public float speed = 10;
    public int damage = 1;

    public float lifeTime = 3;
    private float _lifeTime;
    private float collisionOffset = .1f;

    void Start()
    {
       
    }

    void Update()
    {
        if ((_lifeTime -= Time.deltaTime) <= 0)
        {
            Destroy();
            return;
        }
        float moveDistance = speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, moveDistance + collisionOffset, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit.collider, hit.point);
            //Destroy(Instantiate(hitEffect.gameObject, hit.point, Quaternion.FromToRotation(Vector3.forward, transform.forward) * Quaternion.AngleAxis(180, Vector3.right)) as GameObject, hitEffect.startLifetime);
        }
    }


    private void OnHitObject(Collider c, Vector3 hitPoint)
    {
        IDamageable damageableObject = c.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.Damage(damage);
        }

        Destroy();
    }

    public void Destroy()
    {
        gameObject.SetActive(false);
    }

    public void OnCreate()
    {
        
    }

    public void OnReuse()
    {
        _lifeTime = lifeTime;
        Collider[] initialCollisions = Physics.OverlapSphere(transform.position, .1f, collisionMask);
        if (initialCollisions.Length > 0)
        {
            OnHitObject(initialCollisions[0], transform.position);
        }
    }
}
