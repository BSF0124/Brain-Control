using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCharacter : MonoBehaviour
{
    private bool isMoving = false; // 캐릭터 이동중 확인
    private Vector3 targetPosition, temp;
    private float min_X, max_X, min_Y, max_Y; // 최소, 최대 좌표
    private float sum_X, sum_Y;
    private float column, row;

    MainBoard MainBoard;

    void Start()
    {
        MainBoard = GameObject.Find("MainBoard").GetComponent<MainBoard>();
        SetCoordinates();
        CreateCharacter();
        targetPosition = transform.localPosition;
    }

    void Update()
    { 
        if(!isMoving)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                temp = targetPosition;
                targetPosition -= new Vector3(sum_X, 0, 0);
                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                temp = targetPosition;
                targetPosition += new Vector3(sum_X, 0, 0);
                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                temp = targetPosition;
                targetPosition += new Vector3(0, sum_Y, 0);
                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                temp = targetPosition;
                targetPosition -= new Vector3(0, sum_Y, 0);
                MoveCheck();
            }
        }
    }

    // 캐릭터 생성
    void CreateCharacter()
    {
        // 크기 설정
        Transform parent = transform.parent;
        transform.parent = null;
        transform.localScale = new Vector3(1, 1, 1);
        transform.parent = parent;
        
        // 좌표 설정
        transform.localPosition = new Vector3(min_X, max_Y, 1);
    }

    // 최소, 최대 좌표, sum 설정
    public void SetCoordinates()
    {
        min_X = MainBoard.initial_X;
        max_X = min_X * -1;

        max_Y = MainBoard.initial_Y;
        min_Y = max_Y * -1;

        sum_X = MainBoard.sum_X;
        sum_Y = MainBoard.sum_Y;
    }

    // 캐릭터 이동
    IEnumerator MoveToTarget()
    {
        isMoving = true;

        transform.DOLocalMove(targetPosition, 0.25f);
        yield return new WaitForSeconds(0.1f);

        isMoving = false;
        // while(transform.position != targetPosition)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, targetPosition,
        //     moveSpeed * Time.deltaTime);
        //     yield return null;
        // }
    }

    // 캐릭터 흔들기(이동불가)
    void Shake()
    {
        transform.DOShakePosition(0.3f, 0.05f, 25, 90);
    }

    // 캐릭터 이동 가능 확인(최대 최소 좌표에 있는지, 이미 밟은 발판을 밟는지)
    void MoveCheck()
    {
        if(CheckCoordinates())
        {
            Shake();
            targetPosition = temp;
        }

        else
        {
            StartCoroutine(MoveToTarget());
        }
    }

    // 현재 캐릭터의 위치가 최소, 최대 좌표에 있는지 확인
    bool CheckCoordinates()
    {
        if(targetPosition.x < (min_X-0.005) || targetPosition.x > max_X || 
        targetPosition.y < min_Y || targetPosition.y > (max_Y+0.005))
            return true; // 움직임X
        
        else
            return false; // 움직임O
    }

    bool ActivatedCheck()
    {
        return true;
    }

}