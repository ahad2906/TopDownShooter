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

    
    // Start is called before the first frame update
    void Start() {
        base.Start();
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
    }

    void FixedUpdate() {
        
    }

    // Update is called once per frame
    void Update() {
        base.Update();
    }

    //
    IEnumerator UpdatePath() {
        float refreshRate = .25f;

        while (target != null) {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            pathFinder.SetDestination(targetPosition);
            yield return new WaitForSeconds(refreshRate);
        }
    }

}
