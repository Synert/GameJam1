using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOverUI;

    public bool gameover = false;

    void Start()
    {
        GameOverUI.SetActive(false);
    }

    void Update()
    {
        if (gameover == true)
        {
            Time.timeScale = 0;
        }
        if (gameover == false)
        {
            Time.timeScale = 1;
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
        gameover = false;
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("EndScreen");
    }
}
