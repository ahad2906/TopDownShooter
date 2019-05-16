using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    GameObject[] pauseObjects;

    private bool paused = false;

    Player player;


    void Start()
    {
        //Time.timeScale = 0;

        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");

        //hidePaused();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || (Input.GetKeyDown(KeyCode.JoystickButton9)))
        {
            if (paused)
            { 
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

    public void pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
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

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

}
