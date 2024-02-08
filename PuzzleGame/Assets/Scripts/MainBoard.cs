using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoard : MonoBehaviour
{
    private int column, row;
    private int x, y;
    private char[,] board;
    private bool[,] boardVisited; // 보드의 상태를 추적하는 배열
    public SubCharacter subCharacter;

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
        boardVisited[x, y] = true;

        board = new char[column, row];
        for(int i=0; i<column*row; i++)
        {
            board[i%column,i/column] = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_Elements[i];
        }
    }

    // 보드를 밟음
    public void VisitBoard(int x, int y)
    {
        boardVisited[x, y] = true;
        Board board = GameObject.Find($"{this.x+(this.y*column)}").GetComponent<Board>();
        board.Activated();
    }

    // 보드를 밟았는지 여부를 반환
    public bool IsBoardVisited(int x, int y)
    {
        return boardVisited[x, y];
    }

    public void MoveSubCharacter(int x, int y)
    {
        if(board[x,y] == 'L')
        {
            subCharacter.Move_Left();
        }
        if(board[x,y] == 'R')
        {
            subCharacter.Move_Right();
        }
        if(board[x,y] == 'U')
        {
            subCharacter.Move_Up();
        }
        if(board[x,y] == 'D')
        {
            subCharacter.Move_Down();
        }
        if(board[x,y] == 'G')
        {
            GameManager.instance.isMainClear = true;
        }
    }
}
