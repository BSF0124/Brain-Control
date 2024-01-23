using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageActive : MonoBehaviour
{
    private string currentStage;

    void Awake()
    {
        gameObject.SetActive(false);
        currentStage = "Stage " + (DataManager.instance.currentPlayer.currentStage+1).ToString();
        if(currentStage == gameObject.name)
        {
            gameObject.SetActive(true);
        }
    }
}
