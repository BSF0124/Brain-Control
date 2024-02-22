using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBoards: MonoBehaviour
{
    [SerializeField]
    private GameObject[] board;

    public int column; // 열
    public int row; // 행
    private int[,] type; //발판의 종류를 나타내는 배열
    // 0 : 시작 / 1 : 왼쪽 / 2 : 오른쪽 /3 : 위쪽 / 4 : 아래쪽 / 5 : 도착
    private bool[,] boardVisited; // 보드의 상태를 추적하는 배열

    private float current_X; // 현재 배치할 x좌표 (음수->양수)
    private float current_Y; // 현재 배치할 y좌표 (양수->음수)
    [HideInInspector]
    public float initial_X; // 초기 x좌표 (음수)
    [HideInInspector]
    public float initial_Y; // 초기 y좌표 (양수)
    [HideInInspector]
    public float sum_X; // 발판 사이의 x값
    [HideInInspector]
    public float sum_Y; // 발판 사이의 y값

    void Awake()
    {
        // 보드 크기 설정
        Transform parent = transform.parent;
        transform.parent = null;
        transform.localScale = new Vector3(column, row, 1);
        transform.parent = parent;

        // 보드 종류 설정
        type = new int[,] {
            {0,2,1,2},
            {1,1,1,1},
            {1,2,2,1},
            {2,1,2,5}
        };

        sum_X = (float)(Math.Truncate(1000 / (double)column) / 1000);
        sum_Y = (float)(Math.Truncate(1000 / (double)row) / 1000);

        if (column % 2 == 0)
        {
            initial_X = (sum_X / 2) + ((column / 2 - 1) * sum_X);
            initial_X *= -1;
        }
        else
        {
            initial_X = column / 2 * sum_X;
            initial_X *= -1;
        }

        if (row % 2 == 0)
        {
            initial_Y = (sum_Y / 2) + ((row / 2 - 1) * sum_Y);
        }
        else
        {
            initial_Y = row / 2 * sum_Y;
        }

        current_X = initial_X;
        current_Y = initial_Y;

        // 보드 상태 배열 초기화
        boardVisited = new bool[row, column];
        for (int i=0; i<row; i++)
        {
            for (int j=0; j<column; j++)
            {
                boardVisited[i, j] = false;
            }
        }

        // 보드 배치
        for (int i=0; i<row; i++)
        {
            current_X = initial_X;

            for (int j=0; j<column; j++)
            {
                Vector3 boardPos = new Vector3(current_X, current_Y, 0);

                GameObject myBoard = Instantiate(board[type[i, j]], new Vector3(0, 0, 0), Quaternion.identity);
                myBoard.transform.parent = this.transform;
                myBoard.transform.localPosition = boardPos;

                current_X += sum_X;
            }
            current_Y -= sum_Y;
        }
    }

    // 해당 좌표의 보드를 밟았음을 표시
    public void MarkBoardVisited(Vector2Int boardCoordinates)
    {
        boardVisited[boardCoordinates.y, boardCoordinates.x] = true;
    }

    // 해당 좌표의 보드를 밟았는지 여부를 반환
    public bool IsBoardVisited(Vector2Int boardCoordinates)
    {
        return boardVisited[boardCoordinates.y, boardCoordinates.x];
    }
}
