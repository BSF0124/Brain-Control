using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBoard : MonoBehaviour
{
    public SubCharacter subCharacter;
    public GameObject[] boards; // 보드 종류
    //0:Start, 1:Up 2:Dowm 3:Left 4:Right 5:Goal 6:Wall 7:Tool
    private GameObject[,] setBoards;
    private int column, row; // 보드의 크기
    private char[,] board; // 보드 구성1
    private bool[,] boardVisited; // 보드 밟음 여부
    private float initial_X, initial_Y; // 초기 좌표
    private float sum_X = 1, sum_Y = 1; // 보드 사이 간격
    private int stageIndex;
    static public bool animationEnd = false;

    void Start() 
    {
        // 해당 스테이지 인덱스 설정
        string[] str = transform.parent.parent.transform.name.Split();
        stageIndex = int.Parse(str[1])-1;

        // 보드의 행과 열 설정
        column = DataManager.instance.stageList.stage[stageIndex].board_Width;
        row = DataManager.instance.stageList.stage[stageIndex].board_Height;
        
        // 보드 배열 크기 설정
        setBoards = new GameObject[column, row];
        boardVisited = new bool[column, row];
        board = new char[column, row];

        // 보드 배열 초기화
        for (int i=0; i<column; i++)
        {
            for (int j=0; j<row; j++)
            {
                boardVisited[i, j] = false;
            }
        }
        for(int i=0; i<column*row; i++)
        {
            board[i%column,i/column] = DataManager.instance.stageList.stage[stageIndex].board_Elements[i];
            if(board[i%column,i/column] == 'W')
            {
                VisitBoard(i%column,i/column);
            }
        }

        // 캐릭터 시작 좌표 true 설정
        VisitBoard(DataManager.instance.stageList.stage[stageIndex].board_X,
        DataManager.instance.stageList.stage[stageIndex].board_Y);

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
    }

    void Update()
    {
        if(animationEnd)
        {
            StartCoroutine(CreateBoard());
            animationEnd = false;
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
        switch(board[x,y])
        {
            case 'L':
                subCharacter.Move_Left();
                break;
            case 'R':
                subCharacter.Move_Right();
                break;
            case 'U':
                subCharacter.Move_Up();
                break;
            case 'D':
                subCharacter.Move_Down();
                break;
            case 'T':
                subCharacter.BoardActivated();
                break;
            case 'G':
                GameManager.instance.isMainClear = true;
                break;
        }
    }

    static public void AnimationEnd()
    {
        animationEnd = true;
    }

    // 보드 생성
    IEnumerator CreateBoard()
    {
        for(int i=0; i<column; i++)
        {
            for(int j=0; j<row; j++)
            {
                float rand = Random.Range(4, 8);
                Vector3 targetPosition = new Vector3(initial_X+(i*sum_X), initial_Y-(j*sum_Y), -1);
                switch(board[i,j])
                {
                    case 'S':
                        setBoards[i,j] = Instantiate(boards[0]);
                        break;
                    case 'U':
                        setBoards[i,j] = Instantiate(boards[1]);
                        break;
                    case 'D':
                        setBoards[i,j] = Instantiate(boards[2]);
                        break;
                    case 'L':
                        setBoards[i,j] = Instantiate(boards[3]);
                        break;
                    case 'R':
                        setBoards[i,j] = Instantiate(boards[4]);
                        break;
                    case 'G':
                        setBoards[i,j] = Instantiate(boards[5]);
                        break;
                    case 'W':
                        setBoards[i,j] = Instantiate(boards[6]);
                        break;
                    case 'T':
                        setBoards[i,j] = Instantiate(boards[7]);
                        break;
                }

                setBoards[i,j].transform.parent = transform.parent;

                if(Skeleton.animationSkipped)
                {
                    setBoards[i,j].transform.localPosition = new Vector3(targetPosition.x,targetPosition.y, -1);
                }

                else
                {
                    setBoards[i,j].transform.localPosition = new Vector3(targetPosition.x,targetPosition.y + rand, -1);
                    StartCoroutine(BoardMovement(i,j,targetPosition));
                }
                yield return null;
            }
        }
        StartBoard.BoardSetEnd();
    }

    IEnumerator BoardMovement(int x, int y, Vector3 target)
    {
        while(setBoards[x,y].transform.position.y >= target.y)
        {
            float rand = Random.Range(0.05f, 0.1f);
            setBoards[x,y].transform.position -= new Vector3(0, rand, 0);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        
    }
}
