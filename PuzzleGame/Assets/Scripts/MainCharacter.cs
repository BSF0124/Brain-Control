using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCharacter : MonoBehaviour
{
    private Vector3 targetPosition; // 캐릭터가 이동할 좌표
    private int column = 0, row = 0; // 캐릭터 현재 위치
    private bool isMoving = false, isShaking = false; // 캐릭터 이동 제어
    private MainBoard mainBoard;
    private GameManager gameManager;

    void Start()
    {
        mainBoard = GameObject.Find("MainBoard").GetComponent<MainBoard>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if(gameManager.isGameClear)
        {
            // 엔터키 입력시 씬 이동
            if(Input.GetKeyDown(KeyCode.Return))
            {gameManager.MoveScene();}
        }

        if (isMoving || isShaking)
        {return;} 

        if (Input.GetKeyDown(KeyCode.UpArrow)) // 위쪽 방향키 입력
        {Move_Up();}
        if (Input.GetKeyDown(KeyCode.DownArrow)) // 아래쪽 방향키 입력
        {Move_Down();}
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 왼쪽 방향키 입력
        {Move_Left();}
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 오른쪽 방향키 입력
        {Move_Right();}
    }

    // 위쪽 이동
    void Move_Up() {
        if(row == 0)
        {Shake();}

        else if(mainBoard.IsBoardVisited(column, row-1))
        {Shake();}

        else
        {
            targetPosition += Vector3.up;
            StartCoroutine(MoveToTarget());
            mainBoard.MarkBoardVisited(column, --row);
        }
    }

    // 아래쪽 이동
    void Move_Down() {
        if(row == mainBoard.row-1)
        {Shake();}

        else if(mainBoard.IsBoardVisited(column, row+1))
        {Shake();}

        else
        {
            targetPosition += Vector3.down;
            StartCoroutine(MoveToTarget());
            mainBoard.MarkBoardVisited(column, ++row);
        }
    }

    // 왼쪽 이동
    void Move_Left() {
        if(column == 0)
        {Shake();}

        else if(mainBoard.IsBoardVisited(column-1, row))
        {Shake();}

        else
        {
            targetPosition += Vector3.left;
            StartCoroutine(MoveToTarget());
            mainBoard.MarkBoardVisited(--column, row);
        }
    }

    // 오른쪽 이동
    void Move_Right() {
        if(column == mainBoard.column-1)
        {Shake();}

        else if(mainBoard.IsBoardVisited(column+1, row))
        {Shake();}

        else
        {
            targetPosition += Vector3.right;
            StartCoroutine(MoveToTarget());
            mainBoard.MarkBoardVisited(++column, row);
        }
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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "EndBoard"){
            gameManager.main_Clear = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "EndBoard")
            isMoving = true;
    }
}
