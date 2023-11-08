using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private GameObject[] board;

    [SerializeField]
    private int column = 5; // 열

    [SerializeField]
    private int row = 5; // 행

    private int[,] type; //발판 종류
    // 0 : 시작 / 1 : 왼쪽 / 2 : 오른쪽 /3 : 위쪽 / 4 : 아래쪽 / 5 : 도착

    private float initial_X; // 초기 x좌표 (음수)
    private float initial_Y; // 초기 y좌표 (양수)
    private float current_X; // 현재 배치할 x좌표 (음수->양수)
    private float current_Y; // 현재 배치할 y좌표 (양수->음수)
    private float sum_X; // 발판 사이의 x값
    private float sum_Y; // 발판 사이의 y값


    //메인 캐릭터와 충돌 감지 -> 충돌하면 오브젝트 변경

    // Start is called before the first frame update
    void Start()
    {
        // 보드 크기 설정
        Transform parent = transform.parent;
        transform.parent = null;
        transform.localScale = new  Vector3(column, row, 1);
        transform.parent = parent;

        // 보드 종류 설정
        type = new int[5,5] {
            {0,1,2,1,3},
            {4,4,2,3,1},
            {2,4,3,2,1},
            {2,3,3,4,2},
            {1,2,1,4,5}
        };

        sum_X = (float)(Math.Truncate(1000 / (double)column)/1000);
        sum_Y = (float)(Math.Truncate(1000 / (double)row)/1000);

        if(column % 2 == 0)
        { initial_X = (sum_X / 2) + ((column / 2 - 1) * sum_X); 
        initial_X*=-1; }
        else
        { initial_X = column / 2 * sum_X; 
        initial_X*=-1; }

        if(row % 2 == 0)
        { initial_Y = (sum_Y / 2) + ((row/ 2 - 1) * sum_Y); }
        else
        { initial_Y = row / 2 * sum_Y; }

        current_X = initial_X;
        current_Y = initial_Y;

        // 보드 배치
        for(int i=0; i<row; i++)
        {
            current_X = initial_X;

            for(int j=0; j<column; j++)
            {
                Vector3 boardPos = new Vector3(current_X, current_Y, 0);
                // GameObject myBoard = Instantiate(board[type[i,j]]) as GameObject;
                // myBoard.transform.SetParent(this.transform, false);
                
                GameObject myBoard = Instantiate(board[type[i,j]], new Vector3(0,0,0), Quaternion.identity);
                myBoard.transform.parent = this.transform;

                myBoard.transform.localPosition = boardPos;

                current_X += sum_X;
            }
            current_Y -= sum_Y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }

    // 최소, 최대 좌표 설정
    public void SetCoordinates(ref float min_X, ref float max_X, ref float min_Y, ref float max_Y, ref float sum_X, ref float sum_Y) {
        min_X = initial_X;
        max_X = initial_X * (-1);
        min_Y = initial_Y * (-1);
        max_Y = initial_Y;
        sum_X = this.sum_X;
        sum_Y = this.sum_Y;
    }
}
