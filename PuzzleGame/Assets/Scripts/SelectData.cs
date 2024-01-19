using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class SelectData : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject createPanel;
    public TextMeshProUGUI[] slotText; // 플레이어 이름
    public TextMeshProUGUI newPlayerName; // 새로운 플레이어 이름

    bool[] saveFile = new bool[3]; // true : 데이터 존재, false - 데이터 없음

    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            if(File.Exists(DataManager.instance.path + $"{i}"))
            {
                saveFile[i] = true;
                DataManager.instance.currentSlot = i;
                DataManager.instance.LoadData();

                slotText[i].text = DataManager.instance.currentPlayer.name;
            }

            else
            {
                slotText[i].text = "Empty";
            }
        }

        DataManager.instance.DataClear();
    }

    public void Slot(int num)
    {
        DataManager.instance.currentSlot = num;

        if(saveFile[num])
        {
            DataManager.instance.LoadData();
            GoGame();
        }

        else
        {
            Creat();
        }
    }

    public void Creat()
    {
        createPanel.gameObject.SetActive(true);
    }

    public void HideCreate()
    {
        createPanel.gameObject.SetActive(false);
    }

    public void GameStart()
    {
        startPanel.gameObject.SetActive(true);
    }

    public void HideStart()
    {
        startPanel.gameObject.SetActive(false);
    }

    public void Delete(int num)
    {
        DataManager.instance.currentSlot = num;
        DataManager.instance.DeleteData();
        slotText[num].text = "Empty";
    }

    public void GoGame()
    {
        if(!saveFile[DataManager.instance.currentSlot])
        {
            DataManager.instance.currentPlayer.name = newPlayerName.text;
            DataManager.instance.SaveData();
        }

        SceneManager.LoadScene("World");
    }
}
