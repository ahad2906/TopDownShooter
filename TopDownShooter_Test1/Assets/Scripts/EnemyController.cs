using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (NavMeshAgent))]
public class EnemyController : MonoBehaviour {

    private Rigidbody myRB;
    public float moveSpeed;

    public PlayerController thePlayer;

    NavMeshAgent pathFinder;
    Transform target;

    
    // Start is called before the first frame update
    void Start() {
        pathFinder = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdatePath());
 
       // myRB = GetComponent<Rigidbody>();
        //thePlayer = FindObjectOfType<PlayerController>();
    }

    void FixedUpdate() {
        //myRB.velocity = (transform.forward * moveSpeed);
    }

    // Update is called once per frame
    void Update() {
        
        // Dyrt at kalde dette for hvert frame
        //pathFinder.SetDestination(target.position);

        //transform.LookAt(thePlayer.transform.position);
    }

    IEnumerator UpdatePath() {
        float refreshRate = .25f;

        while (target != null) {
            Vector3 targetPosition = new Vector3(target.position.x, 0, target.position.z);
            pathFinder.SetDestination(targetPosition);
            yield return new WaitForSeconds(refreshRate);
        }
    }

}
