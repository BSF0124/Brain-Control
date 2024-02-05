using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// public enum LadderState
// {
//     None,Up,Down
// }

public class SubCharacter : MonoBehaviour
{
    private Vector3 targetPosition;
    public float sum_X = 1, sum_Y = 1; // 캐릭터 상하좌우 이동 거리
    // private bool ladder_up = false; // 올라갈 수 있는지 확인
    // private bool ladder_down = false; // 내려갈 수 있는지 확인
    // private LadderState ladderState = LadderState.None;

    public int column, row; // 맵의 가로, 세로의 크기
    private int x, y; // 서브 캐릭터의 위치
    private char[,] map; // 맵의 구조
    // S : 시작점, G : 도착점, L : 사다리, W : 벽, _ : 빈 공간


    void Start()
    {
        targetPosition = transform.localPosition;
        
        for(int i=0; i<column; i++)
        {
            for(int j=0; j<row; j++)
            {
                map[i,j] = 'W';
            }
        }
    }

    void Update()
    {
        // if(map[x,y] == 'G')
        // {
        //     GameManager.instance.isSubClear = true;
        // }
        // else
        // {
        //     GameManager.instance.isSubClear = false;
        // }
    }

    public void Move_Left()
    {
        if(map[x-1,y] != 'W')
        {
            x--;
            targetPosition -= new Vector3(sum_X, 0, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
        }

    }
    public void Move_Right()
    {
        if(map[x+1,y] != 'W')
        {
            x++;
            targetPosition += new Vector3(sum_X, 0, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
        }
    }

    // 캐릭터가 사다리 오브젝트와 충돌되어있으면 올라감/내려감
    public void Move_Up()
    {
        // if(ladderState == LadderState.Up)
        // {
        //     targetPosition += new Vector3(0, sum_Y, 0);
        //     transform.DOLocalMove(targetPosition, 0.25f);
        // }

        if(map[x,y] == 'L' && map[x,y+1] != 'W')
        {
            y++;
            targetPosition += new Vector3(0, sum_Y, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
        }
    }
    public void Move_Down()
    {
        // if(ladderState == LadderState.Down)
        // {
        //     targetPosition -= new Vector3(0, sum_Y, 0);
        //     transform.DOLocalMove(targetPosition, 0.25f);
        // }

        if(map[x,y] == 'L' && map[x,y-1] != 'W')
        {
            y--;
            targetPosition += new Vector3(0, sum_Y, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if(other.gameObject.tag == "ladder_Up")
        //     ladderState = LadderState.Up;

        // if(other.gameObject.tag == "ladder_Down")
        //     ladderState = LadderState.Down;
        
        // if(other.gameObject.tag == "Goal")
        //     GameManager.instance.isSubClear = true;
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        // if(other.gameObject.tag == "ladder_Up" || other.gameObject.tag == "ladder_Down")
        //     ladderState = LadderState.None;
        
        // if(other.gameObject.tag == "Goal")
        //     GameManager.instance.isSubClear = false;
    }
}
