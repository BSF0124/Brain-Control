using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class MainCharacter : MonoBehaviour
{
    private bool isMoving = false;
    private bool isShaking = false;
    private Vector3 targetPosition, temp;
    private float min_X, max_X, min_Y, max_Y; // 최소, 최대 좌표

    MainBoard mainBoard;
    GameManager gameManager;

    void Start()
    {
        mainBoard = GameObject.Find("MainBoard").GetComponent<MainBoard>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        SetCoordinates();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (!isMoving && !isShaking)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                temp = targetPosition;
                targetPosition += Vector3.left;
                MoveCheck();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                temp = targetPosition;
                targetPosition += Vector3.right;
                MoveCheck();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                temp = targetPosition;
                targetPosition += Vector3.up;
                MoveCheck();
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                temp = targetPosition;
                targetPosition += Vector3.down;
                MoveCheck();
            }
        }
    }

    // 최소, 최대 좌표 설정
    void SetCoordinates()
    {
        if(mainBoard.column % 2 == 0)
            max_X = mainBoard.column / 2 - 0.5f;
        else
            max_X = mainBoard.column / 2;

        if(mainBoard.row % 2 == 0)
            max_Y = mainBoard.row / 2 - 0.5f;
        else
            max_Y = mainBoard.row / 2;

        min_X = max_X * -1;
        min_Y = max_Y * -1;

        min_Y -= 2.5f;
        max_Y -= 2.5f;
    }
    

    // 캐릭터 이동
    IEnumerator MoveToTarget()
    {
        isMoving = true;

        transform.DOMove(targetPosition, 0.25f);
        yield return new WaitForSeconds(0.23f);

        isMoving = false;
    }

    // 캐릭터 흔들기(이동불가)
    void Shake()
    {
        isShaking = true;
        transform.DOShakePosition(0.3f, 0.05f, 25, 90).OnComplete(() =>
        {
            isShaking = false;
        });
    }

    // 캐릭터 이동 가능 확인(최대 최소 좌표에 있는지, 이미 밟은 발판을 밟는지)
    void MoveCheck()
    {
        Vector2Int boardCoordinates = GetBoardCoordinates(targetPosition);

        if (CheckCoordinates() || mainBoard.IsBoardVisited(boardCoordinates))
        {
            Shake();
            targetPosition = temp;
        }
        else
        {
            StartCoroutine(MoveToTarget());
            mainBoard.MarkBoardVisited(boardCoordinates);
        }
    }

    // 현재 캐릭터의 위치가 최소, 최대 좌표에 있는지 확인
    bool CheckCoordinates()
    {
        if (targetPosition.x < min_X || targetPosition.x > max_X ||
            targetPosition.y < min_Y || targetPosition.y > max_Y)
            return true; // 움직임X

        else
            return false; // 움직임O
    }

    // 현재 캐릭터의 위치에 해당하는 보드의 좌표 반환
    Vector2Int GetBoardCoordinates(Vector3 position)
    {
        int x = Mathf.FloorToInt(position.x + max_X);
        int y = Mathf.FloorToInt(position.y + max_Y + 5);
        return new Vector2Int(x, y);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "GoalBoard")
            gameManager.main_Clear = true;
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "GoalBoard")
            isMoving = true;
    }
}
