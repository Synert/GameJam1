using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

//used to overwrite gameover rather than chaning original script
public class TwitchGameOverMenu : GameOverMenu
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
		    SceneManager.LoadScene ("TwitchMultiplayer");
		    Time.timeScale = 1;
        }
    }
}
