using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyController : LivingEntity {

    public enum State { Peace, Chasing, Attacking };
    State currentState;

    NavMeshAgent pathFinder;
    Transform target;

    float attackDistance =1.0f;
    float timeBetweenAttacks = 1;

    float nextAttackTime;
    float enemyCollisionRadius;
    float targetColliionRadius;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        currentState = State.Chasing;
        target = GameObject.FindGameObjectWithTag("Player").transform;

        enemyCollisionRadius = GetComponent<CapsuleCollider>().radius;
        targetColliionRadius = target.GetComponent<CapsuleCollider>().radius;

        StartCoroutine(UpdatePath());
    }

    void FixedUpdate() {    
        
    }

    // Update is called once per frame
    void Update() {
        base.Update();
        if (Time.time > nextAttackTime) {
            float distanceFromTarget = (target.position - transform.position).sqrMagnitude;
            if (distanceFromTarget < Mathf.Pow(attackDistance + enemyCollisionRadius + targetColliionRadius, 2)) {
                nextAttackTime = Time.time + timeBetweenAttacks;
                StartCoroutine(Attack());

            }
        }
    }

    void onTargetDeath() {
        currentState = State.Peace;
    }

    IEnumerator Attack() {

        currentState = State.Attacking;
        pathFinder.enabled = false;

        Vector3 attackStartPosition = transform.position;
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 attackEndPosition = target.position - directionToTarget * (enemyCollisionRadius);

        float attackSpeed = 3;
        float percent = 0;

        while(percent <= 1) {

            percent += Time.deltaTime + attackSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector3.Lerp(attackStartPosition, attackEndPosition, interpolation);

            yield return null;
        }
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }
 
    //
    IEnumerator UpdatePath() {
        float refreshRate = .5f;

        while (target != null) {
            if (currentState == State.Chasing) {
                Vector3 directionToTarget = (target.position - transform.position).normalized;
                Vector3 targetPosition = target.position - directionToTarget * (enemyCollisionRadius + targetColliionRadius + attackDistance/2);
                if(!isDead) {
                    pathFinder.SetDestination(targetPosition);
                }
            }
            yield return new WaitForSeconds(refreshRate);
        }
    }

}
