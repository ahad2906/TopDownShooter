using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    GameObject[] pauseObjects;



    private bool paused = false;


    private void Start()
    {
        Time.timeScale = 1;

        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");

        hidePaused();

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.JoystickButton9)))
        {
            if (paused) { 
                Time.timeScale = 1;
                hidePaused();
            }
            else
            {
                Time.timeScale = 0;
                showPaused();
            }
            paused = !paused;
        }

    }


    public void showPaused()
    {
        foreach (GameObject gameObject in pauseObjects)
        {
            gameObject.SetActive(true);
        }
    }

    public void hidePaused()
    {
        foreach (GameObject gameObject in pauseObjects)
        {
            gameObject.SetActive(false);
        }
    }

}
