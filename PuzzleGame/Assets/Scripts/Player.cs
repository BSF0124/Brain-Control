using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    private GameObject stage;
    private string stageName; // 스테이지 이름
    private int currentStage = 1; // 현재 스테이지
    public int lastStage = 3; // 마지막 스테이지

    void Start()
    {
        stageName = "Stage " + currentStage;
        transform.position = GameObject.Find(stageName).transform.position;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(currentStage != 1)
                MoveToPreviousStage();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(currentStage != lastStage)
                MoveToNextStage();
        }
    }
    void MoveToPreviousStage()
    {
        currentStage--;
        stageName = "Stage " + currentStage;
        stage = GameObject.Find(stageName);
        stage.GetComponent<Stage>().MoveLeft(transform);
    }

    void MoveToNextStage()
    {
        stage = GameObject.Find(stageName);
        stage.GetComponent<Stage>().MoveRight(transform);
        currentStage++;
        stageName = "Stage " + currentStage;
    }
}
