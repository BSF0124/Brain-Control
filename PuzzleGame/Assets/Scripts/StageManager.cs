using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public Camera mainCamera;
    private StageActive stageActive;

    void Awake()
    {
        stageActive = GameObject.Find("Stage " + (PlayerMove.currentStage+1).ToString()).GetComponent<StageActive>();
        stageActive.isActive = true;
        mainCamera.transform.position = new Vector3(PlayerMove.currentStage*40, mainCamera.transform.position.y, mainCamera.transform.position.z);
        FadeManager.instance.FadeImage(1, 0, false);
    }
}
