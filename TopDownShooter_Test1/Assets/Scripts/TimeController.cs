using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{

    private bool paused = false;

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.JoystickButton9)))
        {
            if (paused)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;

            paused = !paused;

        }

    }
}
