using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : LivingEntity
{
    public float moveSpeed = 5;
    private bool isSprintng = true;

    private Camera mainCamera;
    private Gun gun;
    private Rigidbody rigidBody;
    private Vector3 velocity, velocitySprint;

    public override void OnCreate()
    {
        base.OnCreate();

        mainCamera = Camera.main;
        rigidBody = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<Gun>();
    }
    protected override void Start()
    {
        //Temporary fix
        OnCreate();
    }

    protected override void Update()
    {
        base.Update();

        // Bevægelse
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        velocity = moveInput.normalized * moveSpeed;
        velocitySprint = moveInput.normalized * (moveSpeed * 2f);


        // Retning af spilleren
        if (ControllerDetector.Instance.IsUsingController())
        {
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        }
        else
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.x -= Screen.width / 2;
            mousePos.y -= Screen.height / 2;

            mousePos += transform.position;
            float angle = Vector3.Angle(mousePos, Vector3.up);

            if (mousePos.x > 0) angle = 360 - angle;

            transform.rotation = Quaternion.Euler(0, -angle, 0);
        }


        // Skydning
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Joystick1Button5)) && isSprintng == false)
        {
            gun.Shoot();
        }

    }

    void FixedUpdate()
    {
        //Bevæger vores spiller
        if (Input.GetKey(KeyCode.Joystick1Button10) || Input.GetKey(KeyCode.LeftShift))
        {
            isSprintng = true;
            rigidBody.MovePosition(rigidBody.position + velocitySprint * Time.fixedDeltaTime);
        } else
        {
            isSprintng = false;
            rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
        }
        //rigidBody.velocity = velocity ;
    }
}
