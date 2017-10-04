using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject GameOverUI;

    private bool gameover;
	public bool multiplayer = false;

    void Start()
    {
        
        gameover = false;
    }

    void Update()
    {
        /*if(gameover == true)
        {
            Time.timeScale = 0;
        }*/
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		
        if (other.tag == "Player")
        {
			if (!multiplayer) {
				gameover = true;
           
				SceneManager.LoadScene ("EndScreen");
			} else {
				SceneManager.LoadScene ("MultiplayerEndScreen");
			}

			Time.timeScale = 1;
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
