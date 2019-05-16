using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : LivingEntity
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    private bool isSprinting;
    private Gun gun;
    private Rigidbody rigidBody;
    private Vector3 velocity;

    public override void OnCreate()
    {
        base.OnCreate();

        rigidBody = GetComponent<Rigidbody>();
        gun = GetComponentInChildren<Gun>();
    }
    protected override void Start()
    {
        //Da spilleren ikke instantieres med PoolMan (endnu)
        //kaldes OnCreate() her i Start()
        OnCreate();
    }

    protected override void Update()
    {
        base.Update();

        // Bevægelse
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Checker om spilleren trykker sprint knappen
        isSprinting = (Input.GetKey(KeyCode.Joystick1Button10) || Input.GetKey(KeyCode.LeftShift));
        //Beregner velociteten
        velocity = moveInput.normalized * ((isSprinting)? sprintSpeed : moveSpeed);


        // Retning af spilleren
        if (ControllerDetector.Instance.IsUsingController())
        {
            //Beregner retningen af spilleren i forhold til joystickens input
            Vector3 playerDirection = Vector3.right * Input.GetAxisRaw("RHorizontal") + Vector3.forward * -Input.GetAxisRaw("RVertical");
            //Hvis længden af vektoren er mere end 0, skal spilleren roteres
            if (playerDirection.sqrMagnitude > 0.0f)
            {
                //Roterer spilleren
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
        }
        else
        {
            //Henter musens placering på skærmen, (0,0) er nede i venstre hjørne)
            Vector3 mousePos = Input.mousePosition;
            //Rykker positionen så punkt (0,0) svarer til midten af skærmen
            mousePos.x -= Screen.width / 2;
            mousePos.y -= Screen.height / 2;
            //Rykker positionen så det svarer til at midten er hvor spilleren står
            mousePos += transform.position;
            //Beregner vinklen musens position ligger i fra midten
            float angle = Vector3.Angle(mousePos, Vector3.up);
            //Vender vinklen hvis musen ligger i højre halvdel af skærmen
            if (mousePos.x > 0) angle = 360 - angle;
            //Roterer spilleren
            transform.rotation = Quaternion.Euler(0, -angle, 0);
        }


        // Skydning
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Joystick1Button5)) && isSprinting == false)
        {
            gun.Shoot();
        }

    }

    void FixedUpdate()
    {
        //Bevæger vores spiller
        rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
    }
}
