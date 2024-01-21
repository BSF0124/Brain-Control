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
            GoGame();
        }
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
        // slotText[num].text = "Empty";
        Refresh();
    }

    public void GoGame()
    {
        if(!saveFile[DataManager.instance.currentSlot])
        {
            DataManager.instance.SaveData();
        }

        SceneManager.LoadScene("World");
    }
}
