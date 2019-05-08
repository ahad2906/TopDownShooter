using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyController : LivingEntity {

    private Rigidbody myRB;
    public float moveSpeed;

    public PlayerController thePlayer;

    NavMeshAgent pathFinder;
    Transform target;

    public enum State {Peace, Chasing, Attacking};
    State currentState; 

    float attackDistance = 1.5f;
    float timeBetweenAttacks = 1;

    float nextAttackTime;

    // Start is called before the first frame update
    protected virtual void Start() {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }

    void FixedUpdate() {
        
    }

    // Update is called once per frame
    public void Update() {
        base.Update();

        float distanceFromTarget = (target.position - transform.position).sqrMagnitude;

        if(distanceFromTarget < Mathf.Pow (attackDistance, 2)) {
            nextAttackTime = Time.time + timeBetweenAttacks;

        }
    }

    IEnumerator Attack() {

        currentState = State.Attacking;
        pathFinder.enabled = false;

        Vector3 attackStartPosition = transform.position;
        Vector3 attackEndPosition = target.position;

        int attackSpeed = 3;
        float attatckStartEndDistance = 0;

        while(attatckStartEndDistance <= 1) {

            attatckStartEndDistance += Time.deltaTime + attackSpeed;
            float interpolation = 4 * (-Mathf.Pow(attatckStartEndDistance, 2) + attatckStartEndDistance);
            transform.position = Vector3.Lerp(attackStartPosition, attackEndPosition, interpolation);

            yield return null;
        }
        currentState = State.Chasing;
        pathFinder.enabled = true;
    }
 
    //
    IEnumerator UpdatePath() {
        float refreshRate = .25f;

        while (target != null) {
            if (currentState == State.Chasing) {
                Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
                pathFinder.SetDestination(targetPosition);
                yield return new WaitForSeconds(refreshRate);
            }
        }
    }

}
