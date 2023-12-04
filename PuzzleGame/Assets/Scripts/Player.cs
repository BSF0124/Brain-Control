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
        Vector3 startPosition = GameObject.Find(stageName).transform.position;
        startPosition.y += 0.17f;
        transform.position = startPosition;
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

    // 이전 스테이지로 이동
    void MoveToPreviousStage()
    {
        currentStage--;
        stageName = "Stage " + currentStage;
        stage = GameObject.Find(stageName);
        stage.GetComponent<Stage>().MoveLeft(transform);
    }

    // 다음 스테이지로 이동
    void MoveToNextStage()
    {
        stage = GameObject.Find(stageName);
        stage.GetComponent<Stage>().MoveRight(transform);
        currentStage++;
        stageName = "Stage " + currentStage;
    }
}
