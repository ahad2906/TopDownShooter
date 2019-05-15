using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : LivingEntity
{
    public enum State { Idling, Chasing, Attacking };
    private State currentState;
    private NavMeshAgent pathfinder;
    private LivingEntity target;

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
    }
    public override void OnCreate()
    {
        base.OnCreate();
        pathfinder = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<Player>().GetComponent<LivingEntity>();
        if (target != null)
        {
            target.OnDeath += OnTargetDeath;

            collisionRadius = GetComponent<CapsuleCollider>().radius;
            targetCollisionRadius = target.GetComponent<CapsuleCollider>().radius;
        }

        //Tilmelder vores DungeonMaster som listener
        OnDeath += FindObjectOfType<DungeonMaster>().OnEnemyDeath;
    }

    public override void OnReuse()
    {
        base.OnReuse();
        if (target != null)
        {
            hasTarget = true;
            currentState = State.Chasing;
            pathfinder.enabled = true;

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
                float sqrDstToTarget = (target.transform.position - transform.position).sqrMagnitude;
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
        StopAllCoroutines();
        hasTarget = false;
        currentState = State.Idling;
    }
    // Kunne måske laves som en ekstern klasse så man kan skifte attack?
    private IEnumerator Attack()
    {
        currentState = State.Attacking;
        pathfinder.enabled = false;

        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = target.transform.position;

        float attackSpeed = 3;
        float percent = 0;

        bool hasAppliedDamage = false;

        while (percent <= 1)
        {
            float heightDifToTarget = targetPosition.y - transform.position.y;
            if (percent >= .5f && !hasAppliedDamage && heightDifToTarget < .5f)
            {
                hasAppliedDamage = true;
                target.Damage(damage);
            }
            percent += Time.deltaTime * attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(originalPosition, targetPosition, interpolation);

            yield return null;
        }

        currentState = State.Chasing;
        pathfinder.enabled = true;

    }

    private IEnumerator UpdatePath()
    {
        float refreshRate = .5f;

        while (hasTarget)
        {
            if (currentState == State.Chasing)
            {
                Vector3 targetPos = target.transform.position;
                Vector3 dirToTarget = (targetPos - transform.position).normalized;
                Vector3 targetPosition = targetPos - dirToTarget * (collisionRadius + targetCollisionRadius);
                if (!isDead)
                {
                    pathfinder.SetDestination(targetPosition);
                }
            }

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
