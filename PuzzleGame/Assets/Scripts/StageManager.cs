using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Camera mainCamera;
    private StageActive stageActive;

    void Awake()
    {
        stageActive = GameObject.Find("Stage " + (DataManager.instance.currentPlayer.currentStage+1).ToString()).GetComponent<StageActive>();
        stageActive.isActive = true;
        mainCamera.transform.position = new Vector3(DataManager.instance.currentPlayer.currentStage*40, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }
}
