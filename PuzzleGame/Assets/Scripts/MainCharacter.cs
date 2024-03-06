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
    private float duration = 0.1f;
    private int stageIndex;
    private MainBoard mainBoard;

    void Start()
    {
        string[] str = transform.parent.parent.transform.name.Split();
        stageIndex = int.Parse(str[1])-1;

        x = DataManager.instance.stageList.stage[stageIndex].board_X;
        y = DataManager.instance.stageList.stage[stageIndex].board_Y;

        mainBoard = GameObject.Find("MainBoard").GetComponent<MainBoard>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if(PauseMenu.isGamePaused)
        {
            return;
        }
        
        if(GameManager.instance.isSceneMove)
        {
            return;
        }

        if(GameManager.instance.isGameClear)
        {
            // 엔터키 입력시 씬 이동
            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.instance.isSceneMove = true;
                StartCoroutine(GoWorld());
            }
        }

        if (isMoving || isShaking || GameManager.instance.isMainClear)
        {
            return;
        } 

        if (Input.GetKeyDown(KeyCode.UpArrow)) // 위쪽 방향키 입력
        {
            Move(0, -1, Vector3.up);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // 아래쪽 방향키 입력
        {
            Move(0, 1, Vector3.down);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 왼쪽 방향키 입력
        {
            Move(-1, 0, Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 오른쪽 방향키 입력
        {
            Move(1, 0, Vector3.right);
        }
    }

    void Move(int column, int row, Vector3 direction)
    {
        if (x + column < 0 || x + column >= DataManager.instance.stageList.stage[stageIndex].board_Width ||
        y + row < 0 || y + row >= DataManager.instance.stageList.stage[stageIndex].board_Height ||
        mainBoard.IsBoardVisited(x + column, y + row))
        {Shake();}

        else
        {
        targetPosition += direction;
        StartCoroutine(MoveToTarget());
        mainBoard.VisitBoard(x += column, y += row);
        mainBoard.MoveSubCharacter(x,y);
        }
    }

    // 캐릭터 이동
    IEnumerator MoveToTarget()
    {
        isMoving = true;

        transform.DOMove(targetPosition, duration);
        yield return new WaitForSeconds(duration);

        isMoving = false;
    }

    // 캐릭터 흔들기(이동불가)
    void Shake()
    {
        isShaking = true;
        transform.DOShakePosition(duration, 0.05f, 25, 90).OnComplete(() =>
        {
            isShaking = false;
        });
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
}
