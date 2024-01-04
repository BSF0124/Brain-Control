using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    public Camera mainCamera; // 메인 카메라
    public float moveSpeed; // 플레이어 이동 속도
    public int maxWorldIndex; // 최대 월드 수
    private int maxStageIndex; // 현재 월드에 해당하는 최대 스테이지 수
    private int worldIndex = 1; // 현재 월드 인덱스
    private int stageIndex = 0; // 현재 스테이지 인덱스
    private bool isMoving = false; // 플레이어가 이동 중인지 확인

    GameObject world;

    void Start()
    {
        world = GameObject.Find("World " + worldIndex);
        maxStageIndex = world.GetComponent<StageManager>().stage.Length;
    }

    void Update()
    {
        if(!isMoving)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if(stageIndex > 0)
                {
                    stageIndex--;
                    MoveStage();
                }
                else if(worldIndex > 1)
                {
                    worldIndex--;
                    MoveWorld();
                }
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                if(stageIndex < maxStageIndex - 1)
                {
                    stageIndex++;
                    MoveStage();
                }
                else if(worldIndex < maxWorldIndex)
                {
                    worldIndex++;
                    MoveWorld();
                }
            }
        }
    }

    public void MoveStage()
    {
        isMoving = true;

        Vector3 stagePosition = world.GetComponent<StageManager>().stage[stageIndex].transform.position;
        transform.DOMove(stagePosition, moveSpeed).OnComplete(() => isMoving = false);
    }

    public void MoveWorld()
    {
        isMoving = true;

        world = GameObject.Find("World " + worldIndex);
        maxStageIndex = world.GetComponent<StageManager>().stage.Length;

        // 메인 카메라 이동 구현
        Vector3 worldPosition = new Vector3((worldIndex - 1) * 20, mainCamera.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.DOMove(worldPosition, moveSpeed);

        if(stageIndex == 0)
            stageIndex = maxStageIndex - 1;
        else
            stageIndex = 0;
        
        MoveStage();

        isMoving = false;
    }
}
