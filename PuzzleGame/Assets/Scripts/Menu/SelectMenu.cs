using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class SelectMenu : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject optionPanel;
    public TextMeshProUGUI[] slotText;

    public TextMeshProUGUI[] texts;
    public int currentButton = 0;

    bool[] saveFile = new bool[3]; // true : 데이터 존재, false : 데이터 없음

    void Start()
    {
        Refresh();
    }

    void Update()
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
            switch(currentButton)
            {
                case 0:
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
            if(startPanel.activeSelf)
            {
                HideStartPanel();
            }

            else if(optionPanel.activeSelf)
            {
                HideOptionPanel();
            }

            else
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
    }

    public void MouseEnter(int index)
    {
        currentButton = index;
        ChangeColor();
    }
}
