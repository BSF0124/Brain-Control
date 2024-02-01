using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] stage;
    public Camera mainCamera;
    void Awake()
    {
        stage[PlayerMove.currentStage].gameObject.SetActive(true);
        mainCamera.transform.position = new Vector3(PlayerMove.currentStage*40, mainCamera.transform.position.y, mainCamera.transform.position.z);
        FadeManager.instance.FadeImage(1, 0, false);
    }
}
