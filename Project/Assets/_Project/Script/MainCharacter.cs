using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCharacter : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 targetPosition;
    private Vector3 temp;

    private float min_X;
    private float max_X;
    private float min_Y;
    private float max_Y;

    Board Board;

    void Start()
    {
        targetPosition = transform.position;
        Board = GameObject.Find("MainBoard").GetComponent<Board>();
        SetCoordinates();
    }

    void Update()
    { 
        if(!isMoving)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {

                temp = targetPosition;
                targetPosition += Vector3.left;
                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                temp = targetPosition;
                targetPosition += Vector3.right;
                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                temp = targetPosition;
                targetPosition += Vector3.up;
                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                temp = targetPosition;
                targetPosition += Vector3.down;
                MoveCheck();
            }
        }
    }

    IEnumerator MoveToTarget()
    {
        isMoving = true;
        // while(transform.position != targetPosition)
        // {
        //     transform.position = Vector3.MoveTowards(transform.position, targetPosition,
        //     moveSpeed * Time.deltaTime);
        //     yield return null;
        // }
        transform.DOMove(targetPosition, 0.25f);
        yield return new WaitForSeconds(0.1f);

        isMoving = false;
    }

    void Shake()
    {
        transform.DOShakePosition(0.3f, 0.05f, 25, 90);
        /*
        float duration : 흔들리는 시간
        float/Vector3 strength : 흔들리는 강도
        int vibrato : 얼마나 흔들릴지
        float randomness : 흔들리는 방향
        */
    }

    void MoveCheck()
    {
        if(CheckCoordinates(targetPosition))
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
    public bool CheckCoordinates(Vector3 Position)
    {
        // 최소, 최대 좌표 설정
        if(Position.x < min_X || Position.x > max_X || Position.y < min_Y || Position.y > max_Y)
            return true; // 움직임X
        
        else
            return false; // 움직임O
    }

    public void SetCoordinates()
    {
        min_X = transform.position.x;
        max_X = min_X + Board.column - 1;

        max_Y = transform.position.y;
        min_Y = max_Y - Board.row + 0.5f;

        Debug.Log(min_X);
        Debug.Log(max_X);

        Debug.Log(min_Y);
        Debug.Log(max_Y);
    }
}
