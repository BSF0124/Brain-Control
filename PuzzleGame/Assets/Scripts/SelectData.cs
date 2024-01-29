using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class SelectData : MonoBehaviour
{
    public GameObject startPanel;
    public TextMeshProUGUI[] slotText;

    bool[] saveFile = new bool[3]; // true : 데이터 존재, false - 데이터 없음

    void Start()
    {
        Refresh();
    }
    
    // 데이터 새로고침
    public void Refresh()
    {
        for(int i = 0; i < 3; i++)
        {
            if(File.Exists(DataManager.instance.path + $"{i}"))
            {
                saveFile[i] = true;
                DataManager.instance.currentSlot = i;
                DataManager.instance.LoadData();

                slotText[i].text = "Player " + (i+1).ToString();
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
            StartCoroutine(GoGame());
        }
    }

    // 데잍 선택창 표시
    public void GameStart()
    {
        startPanel.gameObject.SetActive(true);
    }

    // 데이터 선택창 숨김
    public void HideStart()
    {
        startPanel.gameObject.SetActive(false);
    }

    // 데이터 삭제
    public void Delete(int num)
    {
        DataManager.instance.currentSlot = num;
        DataManager.instance.DeleteData();
        Refresh();
    }

    // 씬 이동
    private IEnumerator GoGame()
    {
        FadeManager.instance.FadeImage(0, 1, true);
        yield return new WaitForSeconds(FadeManager.instance.imageDuration);
        SceneManager.LoadScene("World");
    }
}
