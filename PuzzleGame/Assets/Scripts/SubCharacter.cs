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

    private int column, row; // 맵의 가로, 세로의 크기
    private int x, y; // 서브 캐릭터의 위치
    private char[,] map; // 맵의 구조
    // S : 시작점, G : 도착점, L : 사다리, W : 벽, _ : 빈 공간


    void Start()
    {
        column = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].map_Width;
        row = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].map_Height;
        x = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].map_X;
        y = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].map_Y;

        map = new char[column, row];
        targetPosition = transform.localPosition;
        
        for(int i=0; i<column*row; i++)
        {
            map[i%column,i/column] = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].map_Elements[i];
        }
    }

    void Update()
    {
        if(map[x,y] == 'G')
        {
            GameManager.instance.isSubClear = true;
        }
        else
        {
            GameManager.instance.isSubClear = false;
        }
    }

    public void Move_Left()
    {
        if((x == 0) || (map[x-1,y] == 'W'))
        {

        }
        else
        {
            x--;
            targetPosition -= new Vector3(sum_X, 0, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
        }

    }
    public void Move_Right()
    {
        if((x == column-1) || (map[x+1,y] == 'W'))
        {

        }
        else
        {
            x++;
            targetPosition += new Vector3(sum_X, 0, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
        }
    }

    public void Move_Up()
    {

        if((y == 0) || (map[x,y] != 'L') || (map[x,y-1] == 'W'))
        {

        }
        else
        {
            y--;
            targetPosition += new Vector3(0, sum_Y, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
        }
    }
    public void Move_Down()
    {

        if((y == row-1) || (map[x,y] != 'L') || (map[x,y+1] == 'W'))
        {
            y++;
            targetPosition += new Vector3(0, sum_Y, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
        }
    }
}
