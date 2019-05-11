using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State { Idling, Chasing, Attacking };
    private State currentState;
    //Test
    public IEnumerator attack;

    private NavMeshAgent pathfinder;
    private Transform target;
    private LivingEntity targetEntity;

    //Burde erstattes af IDamageDealer eller Weapon
    public float attackDistance = 2.0f;
    public float timeBetweenAttacks = 1;
    public int damage = 1;

    private float nextAttackTime;
    private float collisionRadius;
    private float targetCollisionRadius;

    private bool hasTarget;
    protected override void Die()
    {
        base.Die();
        StopAllCoroutines();
        Destroy();
    }
    public override void OnCreate()
    {
        base.OnCreate();
    }

    public override void OnReuse()
    {
        base.OnReuse();
        Init();
    }

    protected virtual void Init()
    {
        pathfinder = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<Player>().transform;
        if (target != null)
        {
            hasTarget = true;
            currentState = State.Chasing;

            targetEntity = target.GetComponent<LivingEntity>();
            targetEntity.OnDeath += OnTargetDeath;

            collisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;

            StartCoroutine(UpdatePath());
        }
    }

    protected override void Update()
    {
        base.Update();
        if (hasTarget)
        {
            if (Time.time > nextAttackTime && currentState != State.Idling)
            {
                float sqrDstToTarget = (target.position - transform.position).sqrMagnitude;
                if (sqrDstToTarget < Mathf.Pow(attackDistance, 2))
                {
                    nextAttackTime = Time.time + timeBetweenAttacks;
                    StartCoroutine(Attack());
                }
            }
        }
    }

    private void OnTargetDeath()
    {
        hasTarget = false;
        currentState = State.Idling;
    }
    // Kunne måske laves som en ekstern klasse så man kan skifte attack?
    IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 attackPosition = target.position;

        float attackSpeed = 3;
        float percent = 0;

        bool hasAppliedDamage = false;

        while (percent <= 1)
        {
            float heightDifToTarget = target.position.y - transform.position.y;
            if (percent >= .5f && !hasAppliedDamage && heightDifToTarget < .5f)
            {
                hasAppliedDamage = true;
                targetEntity.Damage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, attackPosition, interpolation);

            yield return null;
        }

        currentState = State.Chasing;
        pathfinder.enabled = true;

    }

    IEnumerator UpdatePath()
    {
        float refreshRate = .5f;

        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - dirToTarget * (collisionRadius + targetCollisionRadius);
                if (!isDead)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
