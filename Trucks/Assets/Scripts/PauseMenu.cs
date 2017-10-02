﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUI;
    public TimeManager timeManager;

    private bool paused = false;

    void Start()
    {

        PauseUI.SetActive(false);


    }

    void Update()
    {

        if (Input.GetButtonDown("Pause"))
        {

            paused = !paused;

            if (paused)
            {

                PauseUI.SetActive(true);
                timeManager.togglePause(true);
            }

            if (!paused)
            {

                PauseUI.SetActive(false);
                timeManager.togglePause(false);
            }

        }

    }

    public void Resume()
    {
        paused = false;
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("EndScreen");
    }
}
