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

    Board Board;

    void Start()
    {
        targetPosition = transform.localPosition;
        Board = GameObject.Find("MainBoard").GetComponent<Board>();
        SetCoordinates();
        CreateCharacter();
    }

    void Update()
    { 
        if(!isMoving)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                temp = targetPosition;
                targetPosition -= new Vector3(sum_X, 0, 0);
                
                Debug.Log(temp);
                Debug.Log(targetPosition);

                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                targetPosition += new Vector3(sum_X, 0, 0);
                
                Debug.Log(temp);
                Debug.Log(targetPosition);

                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                targetPosition += new Vector3(0, sum_Y, 0);
                
                Debug.Log(temp);
                Debug.Log(targetPosition);

                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                targetPosition -= new Vector3(0, sum_Y, 0);
                
                Debug.Log(temp);
                Debug.Log(targetPosition);

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
        transform.localPosition = new Vector3(min_X, max_Y, transform.localPosition.z);
    }

    // 최소, 최대 좌표, sum 설정
    public void SetCoordinates()
    {
        min_X = transform.localPosition.x;
        max_X = transform.localPosition.x * -1;

        max_Y = transform.localPosition.y;
        min_Y = transform.localPosition.y * -1;

        sum_X = Board.sum_X;
        sum_Y = Board.sum_Y;
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
    public bool CheckCoordinates()
    {
        if(targetPosition.x < min_X || targetPosition.x > max_X || 
        targetPosition.y < min_Y || targetPosition.y > max_Y)
            return true; // 움직임X
        
        else
            return false; // 움직임O
    }

}