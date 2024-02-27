using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class SubCharacter : MonoBehaviour
{
    public TextMeshPro distanceTextPrefab;
    private TextMeshPro distanceText;
    private Vector3 targetPosition;

    [HideInInspector]
    public int distance_x, distance_y; // 남은 거리
    public float sum_X = 1, sum_Y = 1; // 캐릭터 이동 거리

    private int column, row; // 맵의 가로, 세로의 크기
    private int x, y; // 서브 캐릭터의 위치
    private int goalCount = 0;
    private bool goalCheck = false;
    private char[,] map; // 맵의 구조
    // S:시작점, G:도착점, L:사다리, W:벽, _:빈 공간
    private float duration = 0.1f;


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
            
            if(goalCount == 0 && (map[i%column, i/column] == 'G' || map[i%column, i/column] == 'B'))
            {
                goalCount++;
                distance_x = i%column;
                distance_y = -(i/column);
            }
            else if(map[i%column, i/column] == 'G' || map[i%column, i/column] == 'B')
            {
                goalCount++;
            }
        }

        distanceText = Instantiate(distanceTextPrefab,gameObject.transform.parent);

        if(goalCount > 1)
        {
            goalCheck = true;
        }

        if(targetPosition.y + 1 >= 8)
        {
            distanceText.transform.localPosition = targetPosition + Vector3.down;
        }
        else
        {
            distanceText.transform.localPosition = targetPosition + Vector3.up;
        }
        distance_x += x;
        distance_y += y;
        DistanceTextUpdate();
        
    }

    void Update()
    {
        if(map[x,y] == 'G' || map[x,y] == 'B')
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
            Shake();
        }
        else
        {
            x--;
            targetPosition -= new Vector3(sum_X, 0, 0);
            if(targetPosition.y + 1 >= 8)
            {
                transform.DOLocalMove(targetPosition, 0.25f);
                distanceText.transform.DOLocalMove(targetPosition + Vector3.down, 0.25f);
            }
            else
            {
                transform.DOLocalMove(targetPosition, 0.25f);
                distanceText.transform.DOLocalMove(targetPosition + Vector3.up, duration);
            }
            distance_x++;
            DistanceTextUpdate();
            
        }

    }
    public void Move_Right()
    {
        if((x == column-1) || (map[x+1,y] == 'W'))
        {
            Shake();
        }
        else
        {
            x++;
            targetPosition += new Vector3(sum_X, 0, 0);
            if(targetPosition.y + 1 >= 8)
            {
                transform.DOLocalMove(targetPosition, 0.25f);
                distanceText.transform.DOLocalMove(targetPosition + Vector3.down, 0.25f);
            }
            else
            {
                transform.DOLocalMove(targetPosition, 0.25f);
                distanceText.transform.DOLocalMove(targetPosition + Vector3.up, duration);
            }
            distance_x--;
            DistanceTextUpdate();
        }
    }

    public void Move_Up()
    {

        if((y == 0) || (map[x,y] != 'L') || (map[x,y] != 'B') || (map[x,y-1] == 'W'))
        {
            Shake();
        }
        else
        {
            y--;
            targetPosition += new Vector3(0, sum_Y, 0);

            if(targetPosition.y + 1 >= 8)
            {
                transform.DOLocalMove(targetPosition, 0.25f);
                distanceText.transform.DOLocalMove(targetPosition + Vector3.down, 0.25f);
            }
            else
            {
                transform.DOLocalMove(targetPosition, 0.25f);
                distanceText.transform.DOLocalMove(targetPosition + Vector3.up, 0.25f);
            }
            distance_y--;
            DistanceTextUpdate();
        }
    }
    public void Move_Down()
    {

        if((y == row-1) || (map[x,y] != 'L') || (map[x,y] != 'B') || (map[x,y+1] == 'W'))
        {
            Shake();
        }
        else
        {
            y++;
            targetPosition -= new Vector3(0, sum_Y, 0);
            if(targetPosition.y + 1 >= 8)
            {
                transform.DOLocalMove(targetPosition, 0.25f);
                distanceText.transform.DOLocalMove(targetPosition + Vector3.down, 0.25f);
            }
            else
            {
                transform.DOLocalMove(targetPosition, 0.25f);
                distanceText.transform.DOLocalMove(targetPosition + Vector3.up, duration);
            }
            distance_y++;
            DistanceTextUpdate();
        }
    }

    void Shake()
    {
        transform.DOShakePosition(duration, 0.05f, 25, 90);
    }
    
    void DistanceTextUpdate()
    {
        if(goalCheck)
        {
            distanceText.text = "?,?";
        }
        else if(distance_x == 0 && distance_y == 0)
        {
            distanceText.text = "Goal!";
        }

        else
        {
            distanceText.text = $"[{distance_x},{distance_y}]";
        }
    }
}
