using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainBoard : MonoBehaviour
{
    public int column, row;
    private bool[,] boardVisited; // 보드의 상태를 추적하는 배열

    void Start()
    {
        //보드 크기 설정
        Transform parent = transform.parent;
        transform.parent = null;
        transform.localScale = new Vector3(column+1, row+1, 1);
        transform.parent = parent;

        // 보드 상태 배열 초기화
        boardVisited = new bool[row, column];
        for (int i=0; i<row; i++)
        {
            for (int j=0; j<column; j++)
            {
                boardVisited[i, j] = false;
            }
        }
        boardVisited[row-1, 0] = true;
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
