using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoard : MonoBehaviour
{
    public static MainBoard instance;
    public int column, row;
    private bool[,] boardVisited; // 보드의 상태를 추적하는 배열

    void Start()
    {
        instance = this;
        
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
