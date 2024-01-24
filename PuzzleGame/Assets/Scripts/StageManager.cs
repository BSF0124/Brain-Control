using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Camera mainCamera;
    // private int stageIndex;
    private StageActive stageActive;

    void Start()
    {
        // stageIndex = DataManager.instance.currentPlayer.currentStage;
        stageActive = GameObject.Find("Stage " + (DataManager.instance.currentPlayer.currentStage+1)).GetComponent<StageActive>();
        stageActive.isActive = true;
        mainCamera.transform.position = new Vector3(DataManager.instance.currentPlayer.currentStage*40, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }
}
