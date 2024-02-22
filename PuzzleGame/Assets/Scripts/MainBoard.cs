using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoard : MonoBehaviour
{
    public SubCharacter subCharacter;
    public GameObject[] boards; // 보드 오브젝트
    private int column, row; // 보드의 크기
    private char[,] board; // 보드 구성
    private bool[,] boardVisited; // 보드 밟음 여부

    private float initial_X, initial_Y; // 초기 좌표
    private float sum_X = 1, sum_Y = 1; // 보드 사이 간격
    private float current_X, current_Y; // 현재 좌표

    void Start()
    {
        column = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_Width;
        row = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_Height;
        
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
        VisitBoard(DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_X,
        DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_Y);

        board = new char[column, row];
        for(int i=0; i<column*row; i++)
        {
            board[i%column,i/column] = DataManager.instance.stageList.stage[DataManager.instance.currentPlayer.stageIndex].board_Elements[i];
            if(board[i%column,i/column] == 'W')
            {
                VisitBoard(i%column,i/column);
            }
        }

        // 보드 배치를 위한 초기 좌표 설정
        if (column % 2 == 0)
        {
            initial_X = column / 2 - 0.5f;
            initial_X *= -1;
        }
        else
        {
            initial_X = column / 2;
            initial_X *= -1;
        }

        if (row % 2 == 0)
        {
            initial_Y = (row-1) * 0.5f - 2.5f;
        }
        else
        {
            initial_Y = row / 2 - 2.5f;
        }
        current_X = initial_X;
        current_Y = initial_Y;

        // 보드 배치
        for(int i=0; i<row; i++)
        {
            current_X = initial_X;
            for(int j=0; j<column; j++)
            {
                Vector3 position = new Vector3(current_X, current_Y, -1);
                if(board[j,i] == 'S')
                {
                    Instantiate(boards[0], position, Quaternion.identity, parent);
                }
                else if(board[j,i] == 'U')
                {
                    Instantiate(boards[1], position, Quaternion.identity, parent);
                }
                else if(board[j,i] == 'D')
                {
                    Instantiate(boards[2], position, Quaternion.identity, parent);
                }
                else if(board[j,i] == 'L')
                {
                    Instantiate(boards[3], position, Quaternion.identity, parent);
                }
                else if(board[j,i] == 'R')
                {
                    Instantiate(boards[4], position, Quaternion.identity, parent);
                }
                else if(board[j,i] == 'G')
                {
                    Instantiate(boards[5], position, Quaternion.identity, parent);
                }
                else if(board[j,i] == 'W')
                {
                    Instantiate(boards[6], position, Quaternion.identity, parent);
                }
                current_X += sum_X;
            }
            current_Y -= sum_Y;
        }
    }

    // 보드를 밟음
    public void VisitBoard(int x, int y)
    {
        boardVisited[x, y] = true;
    }

    // 보드를 밟았는지 여부를 반환
    public bool IsBoardVisited(int x, int y)
    {
        return boardVisited[x, y];
    }

    // 밟은 보드에 따른 서브 캐릭터 이동
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
