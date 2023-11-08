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
    private float sum_X;
    private float sum_Y;

    GameObject Board;

    void Start()
    {
        targetPosition = transform.localPosition;
        Board = GameObject.Find("MainBoard");
        Board.GetComponent<Board>().SetCoordinates(ref min_X, ref max_X, ref min_Y, ref max_Y, ref sum_X, ref sum_Y);
    }

    void Update()
    { 
        if(!isMoving)
        {
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Debug.Log(sum_X);
                Debug.Log(targetPosition);
                temp = targetPosition;
                targetPosition += new Vector3(sum_X, 0, 0);
                Debug.Log(targetPosition);
                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                temp = targetPosition;
                targetPosition += new Vector3((-1*sum_X), 0, 0);
                Debug.Log(targetPosition);
                MoveCheck();
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                targetPosition += Vector3.up;
                StartCoroutine(MoveToTarget());
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                targetPosition += Vector3.down;
                StartCoroutine(MoveToTarget());
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
            return true;
        
        else
            return false;
    }

    
}
