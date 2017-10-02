﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOverUI;

    private bool gameover;

    void Start()
    {
        GameOverUI.SetActive(false);
        gameover = false;
    }

    void Update()
    {
        if(gameover == true)
        {
            Time.timeScale = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameover = true;
            GameOverUI.SetActive(true);
        }
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
