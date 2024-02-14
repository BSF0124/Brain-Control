using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject optionPanel;
    public static bool isGamePaused = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        PauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void Pause()
    {
        PauseMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void ShowOptionPanel()
    {
        optionPanel.gameObject.SetActive(true);
    }

    public void HideOptionPanel()
    {
        optionPanel.gameObject.SetActive(false);
    }

    public void World()
    {
        SceneManager.LoadScene("World");
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
