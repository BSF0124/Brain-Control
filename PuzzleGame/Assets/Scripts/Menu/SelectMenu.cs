using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System;

public class SelectMenu : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject optionPanel;
    public TextMeshProUGUI[] slotText;

    public TextMeshProUGUI[] texts;
    public Button[] slotButton;

    [HideInInspector]
    public int currentButton = 0;
    [HideInInspector]
    public int currentSlot = 0;

    bool[] saveFile = new bool[3]; // true : 데이터 존재, false : 데이터 없음

    void Start()
    {
        ChangeTextColor();
        Refresh();
    }

    void Update()
    {
        if(startPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                HideStartPanel();
                currentSlot = 0;
                ChangeButtonColor();
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(currentSlot % 2 == 1)
                {
                    currentSlot--;
                    ChangeButtonColor();
                }
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(currentSlot % 2 == 0)
                {
                    currentSlot++;
                    ChangeButtonColor();
                }
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(currentSlot > 1)
                {
                    currentSlot -= 2;
                    ChangeButtonColor();
                }
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(currentSlot < 4)
                {
                    currentSlot += 2;
                    ChangeButtonColor();
                }
            }

            if(Input.GetKeyDown(KeyCode.Return))
            {
                switch(currentSlot)
                {
                    case 0:
                        Slot(0);
                        break;
                    case 1:
                        Delete(0);
                        break;
                    case 2:
                        Slot(1);
                        break;
                    case 3:
                        Delete(1);
                        break;
                    case 4:
                        Slot(2);
                        break;
                    case 5:
                        Delete(2);
                        break;
                }
            }
        }

        else if(optionPanel.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                HideOptionPanel();
            }
        }

        else
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                if(currentButton != 0)
                {
                    currentButton--;
                    ChangeTextColor();
                }
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                if(currentButton != texts.Length-1)
                {
                    currentButton++;
                    ChangeTextColor();
                }
            }

            if(Input.GetKeyDown(KeyCode.Return))
            {
                switch(currentButton)
                {
                    case 0:
                        currentSlot = 0;
                        ChangeButtonColor();
                        ShowStartPanel();
                        break;
                    case 1:
                        ShowOptionPanel();
                        break;
                    case 2:
                        Quit();
                        break;
                }
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Quit();
            }
        }
    }
    
    // 데이터 새로고침
    public void Refresh()
    {
        for(int i = 0; i < 3; i++)
        {
            if(File.Exists($"{DataManager.instance.path}/save{i}.json"))
            {
                saveFile[i] = true;
                DataManager.instance.currentSlot = i;
                DataManager.instance.LoadData();

                slotText[i].text = $"Player {i+1}";
            }

            else
            {
                saveFile[i] = false;
                slotText[i].text = "Empty";
            }
        }

        DataManager.instance.DataClear();
    }

    // 데이터 슬롯 선택 메서드
    public void Slot(int num)
    {
        DataManager.instance.currentSlot = num;

        if(saveFile[num])
        {
            DataManager.instance.LoadData();
            StartCoroutine(GoGame());
        }

        else
        {
            DataManager.instance.SaveData();
            StartCoroutine(GoCutScene());
        }
    }

    // 데이터 선택창 표시
    public void ShowStartPanel()
    {
        startPanel.gameObject.SetActive(true);
    }

    // 데이터 선택창 숨김
    public void HideStartPanel()
    {
        startPanel.gameObject.SetActive(false);
    }

    // 옵션창 표시
    public void ShowOptionPanel()
    {
        optionPanel.gameObject.SetActive(true);
    }

    // 옵션창 숨김
    public void HideOptionPanel()
    {
        optionPanel.gameObject.SetActive(false);
    }

    // 데이터 삭제
    public void Delete(int num)
    {
        DataManager.instance.currentSlot = num;
        DataManager.instance.DeleteData();
        Refresh();
    }

    // 종료
    public void Quit()
    {
        Application.Quit();
    }

    // 씬 이동
    private IEnumerator GoGame()
    {
        FadeManager.instance.FadeImage(0, 1);
        yield return new WaitForSeconds(FadeManager.instance.imageDuration);
        SceneManager.LoadScene("World");
    }

     // 씬 이동
    private IEnumerator GoCutScene()
    {
        FadeManager.instance.FadeImage(0, 1);
        yield return new WaitForSeconds(FadeManager.instance.imageDuration);
        SceneManager.LoadScene("CutScene");
    }

    // 클리어 데이터 변경
    public void ClearSave()
    {
        for(int i=0; i<3; i++)
        {
            DataManager.instance.currentSlot = i;
            DataManager.instance.DeleteData();
            DataManager.instance.SaveData();
            DataManager.instance.currentPlayer.isClear = new bool[]{true,true,true,true,true,true,true,true,
            true,true,true,true,true,true,
            true,true,true,true,true,true,
            true,true,true,true,true,true,
            true,true,true};
        }
        Refresh();
    }

    public void ChangeWhiteText(int index)
    {
        texts[index].color = Color.white;
    }

    public void ChangeGrayText(int index)
    {
        texts[index].color = new Color32(161,161,161,255);
    }

    public void ChangeTextColor()
    {
        for(int i=0; i<texts.Length; i++)
        {
            if(i == currentButton)
                ChangeWhiteText(i);

            else
                ChangeGrayText(i);
        }
    }

    public void MouseEnter(int index)
    {
        currentButton = index;
        ChangeTextColor();
    }

    public void MouseEnterSlot(int index)
    {
        currentSlot = index;
        ChangeButtonColor();
    }

    public void ChangeButtonColor()
    {
        for(int i=0; i<slotButton.Length; i++)
        {
            if(i == currentSlot)
                ChangeWhiteButton(i);

            else
                ChangeGrayButton(i);
        }
    }

    private void ChangeWhiteButton(int index)
    {
        slotButton[index].image.color = Color.white;

    }

    private void ChangeGrayButton(int index)
    {
        slotButton[index].image.color = new Color32(161,161,161,255);
    }
}
