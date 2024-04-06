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

    bool[] saveFile = new bool[3]; // true : 데이터 존재, false : 데이터 없음

    void Start()
    {
        Refresh();
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
        FadeManager.instance.FadeImage(0, 1, true);
        yield return new WaitForSeconds(FadeManager.instance.imageDuration);
        SceneManager.LoadScene("World");
    }

     // 씬 이동
    private IEnumerator GoCutScene()
    {
        FadeManager.instance.FadeImage(0, 1, true);
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
}
