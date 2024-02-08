using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainCharacter : MonoBehaviour
{
    private Vector3 targetPosition; // 캐릭터가 이동할 좌표
    private int x, y; // 캐릭터 현재 위치
    private bool isMoving = false, isShaking = false; // 캐릭터 이동 제어
    private MainBoard mainBoard;

    void Start()
    {
        mainBoard = GameObject.Find("MainBoard").GetComponent<MainBoard>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if(GameManager.instance.isSceneMove)
        {return;}

        if(GameManager.instance.isGameClear)
        {
            // 엔터키 입력시 씬 이동
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                GameManager.instance.isSceneMove = true;
                StartCoroutine(GoWorld());
            }
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

    void Move(int column, int row, Vector3 direction)
    {
        if (x + column < 0 || x + column >= mainBoard.column ||
        y + row < 0 || y + row >= mainBoard.row ||
        mainBoard.IsBoardVisited(x + column, y + row))
        {Shake();}

        else
        {
        targetPosition += direction;
        StartCoroutine(MoveToTarget());
        mainBoard.MarkBoardVisited(x += column, y += row);
        }
    }

    void Move_Up() { Move(0, -1, Vector3.up); }
    void Move_Down() { Move(0, 1, Vector3.down); }
    void Move_Left() { Move(-1, 0, Vector3.left); }
    void Move_Right() { Move(1, 0, Vector3.right); }

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

    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "EndBoard")
        {
            isMoving = true;
            GameManager.instance.isMainClear = true;
        }
    }

    // 월드 씬 이동
    private IEnumerator GoWorld()
    {
        GameManager.instance.isSceneMove = true;
        FadeManager.instance.FadeLoopStop();
        FadeManager.instance.FadeText(1, 0, true);
        FadeManager.instance.FadeImage(0.9f, 1, true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("World");
        GameManager.instance.ResetBool();
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.gameObject.tag == "EndBoard"){
    //     }
    // }
}
