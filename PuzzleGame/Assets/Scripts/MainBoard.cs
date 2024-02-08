using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoard : MonoBehaviour
{
    public int column, row;
    public int x, y;
    private char[,] board;
    private bool[,] boardVisited; // 보드의 상태를 추적하는 배열

    void Start()
    {
        column = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_Width;
        row = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_Height;
        x = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_X;
        y = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_Y;

        //보드 크기 설정
        Transform parent = transform.parent;
        transform.parent = null;
        transform.localScale = new Vector3(column+1, row+1, 1);
        transform.parent = parent;

        // 보드 상태 배열 초기화
        boardVisited = new bool[column, row];
        for (int i=0; i<column; i++)
        {
            for (int j=0; j<row; j++)
            {
                boardVisited[i, j] = false;
            }
        }
        
        // 캐릭터 시작 좌표 true 설정
        boardVisited[0, 0] = true;

        board = new char[column, row];
        for(int i=0; i<column*row; i++)
        {
            board[i%row,i/4] = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_Elements[i];
        }
    }

    // 해당 인덱스의 보드를 밟았음을 표시
    public void MarkBoardVisited(int column, int row)
    {
        boardVisited[column, row] = true;
    }

    // 해당 인덱스의 보드를 밟았는지 여부를 반환
    public bool IsBoardVisited(int column, int row)
    {
        return boardVisited[column, row];
    }
}
