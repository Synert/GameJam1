using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject PauseUI;
    public TimeManager timeManager;

    private bool paused = false;

    void Start()
    {
		Debug.Log (gameObject.name);
        PauseUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            paused = !paused;

            timeManager.togglePause(paused);
        }

        PauseUI.SetActive(paused);
        
    }

    public void Resume()
    {
        paused = false;
        timeManager.togglePause(paused);
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        paused = false;
        timeManager.togglePause(paused);
        timeManager.restart();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu()
    {
		Time.timeScale = 1;
        SceneManager.LoadScene("start menu");
    }
}
