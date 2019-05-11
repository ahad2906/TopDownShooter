using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerDetector
{
    private static ControllerDetector _instance;
    public static ControllerDetector Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ControllerDetector();
            }
            return _instance;
        }
    }

    private ControllerDetector()
    {

    }

    public bool IsUsingController()
    {
        //Returnerer sandt hvis spilleren bruger controller
        return (Input.GetKey(KeyCode.JoystickButton0) || Input.GetKey(KeyCode.JoystickButton1) || Input.GetKey(KeyCode.JoystickButton2) ||
            Input.GetKey(KeyCode.JoystickButton3) || Input.GetKey(KeyCode.JoystickButton4) || Input.GetKey(KeyCode.JoystickButton5) ||
            Input.GetKey(KeyCode.JoystickButton6) || Input.GetKey(KeyCode.JoystickButton7) || Input.GetKey(KeyCode.JoystickButton8) ||
            Input.GetKey(KeyCode.JoystickButton9) || Input.GetKey(KeyCode.JoystickButton10) || 
            Input.GetAxisRaw("RHorizontal") != 0.0f || Input.GetAxisRaw("RVertical") != 0.0f);
    }
}
