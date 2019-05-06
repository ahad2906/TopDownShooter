using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{

    private Rigidbody myRigidbody;
    public float moveSpeed;

    private Vector3 moveInput;
    private Vector3 moveVelocity;

    private Camera mainCamera;

    public GunController theGun;

    public bool useController;


    // Start is called before the first frame update
    void Start() {
        myRigidbody = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>(); 
    }

    // Update is called once per frame
    void Update() {
        // bruger .GetAxisRaw = fjerner smoothing, bevægelse stopper når man ikke giver input mere
        moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput * moveSpeed;

        // Rotate with Mouse
        if (!useController) {
            Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
            float rayLength;

            if (groundPlane.Raycast(cameraRay, out rayLength)){
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);
                
                //Tegner en streg mod plane hvor mus-pilen peger
                //Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
                // Player kigger mod stregen
                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            }

            // Weapon input "Mouse"
            if (Input.GetMouseButtonDown(0))
                theGun.isFiring = true;

            if (Input.GetMouseButtonUp(0))
                theGun.isFiring = false;
        }

        // Rotate with Controller
        if (useController){
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
            if (playerDirection.sqrMagnitude > 0.0f) {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }

            //´Weapon input "Controller"
            if (Input.GetKeyDown(KeyCode.Joystick1Button5)) {
                theGun.isFiring = true;
            }
            if (Input.GetKeyUp(KeyCode.Joystick1Button5)) {
                theGun.isFiring = false;
            }
        }
    }

    void FixedUpdate() {
        myRigidbody.velocity = moveVelocity;
    }
}