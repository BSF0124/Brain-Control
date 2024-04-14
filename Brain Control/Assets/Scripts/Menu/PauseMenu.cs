using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject optionPanel;
    public static bool isGamePaused = false;

    public TextMeshProUGUI[] texts;
    [HideInInspector]
    public int currentButton = 0;

    void Start()
    {
        ChangeColor();
    }

    void Update()
    {
        if(GameManager.instance.isSceneMove)
            return;

        
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameManager.instance.isGameClear)
            {
                return;
            }

            else if(optionPanel.activeSelf)
            {
                HideOptionPanel();
            }

            else if(isGamePaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }

        if(isGamePaused)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(currentButton != 0)
                {
                    currentButton--;
                    ChangeColor();
                }
            }
    
            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(currentButton != texts.Length-1)
                {
                    currentButton++;
                    ChangeColor();
                }
            }
    
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(texts.Length == 4)
                {
                    switch(currentButton)
                    {
                        case 0:
                            Resume();
                            break;
                        case 1:
                            ShowOptionPanel();
                            break;
                        case 2:
                            MainMenu();
                            break;
                        case 3:
                            Quit();
                            break;
                    }
                }
                else
                {
                    switch(currentButton)
                    {
                        case 0:
                            Resume();
                            break;
                        case 1:
                            ShowOptionPanel();
                            break;
                        case 2:
                            World();
                            break;
                        case 3:
                            MainMenu();
                            break;
                        case 4:
                            Quit();
                            break;
                    }
                }
            }
        }
    }

    public void Resume()
    {
        AudioManager.instance.StartBgm();
        PauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1;
        Invoke("SetGamePause", 0.5f);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.MenuSelect);
    }

    public void Pause()
    {
        AudioManager.instance.StopBgm();
        PauseMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0;
        isGamePaused = true;
    }

    public void ShowOptionPanel()
    {
        optionPanel.gameObject.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.MenuSelect);
    }

    public void HideOptionPanel()
    {
        optionPanel.gameObject.SetActive(false);
    }

    public void World()
    {
        Skeleton.animationSkipped = false;
        SceneManager.LoadScene("World");
        Time.timeScale = 1;
        isGamePaused = false;
    }

    public void MainMenu()
    {
        Skeleton.animationSkipped = false;
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        isGamePaused = false;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.MenuSelect);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeWhite(int index)
    {
        texts[index].color = Color.white;
    }

    public void ChangeGray(int index)
    {
        texts[index].color = new Color32(161,161,161,255);
    }

    public void ChangeColor()
    {
        for(int i=0; i<texts.Length; i++)
        {
            if(i == currentButton)
                ChangeWhite(i);

            else
                ChangeGray(i);
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.MenuChange);
    }

    public void MouseEnter(int index)
    {
        currentButton = index;
        ChangeColor();
    }

    public void SetGamePause()
    {
        isGamePaused = false;
    }
}
