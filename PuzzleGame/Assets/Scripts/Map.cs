using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public float sum_X = 1, sum_Y = 1; // 캐릭터 이동 거리
    private int column, row; // 맵의 가로, 세로의 크기

    private char[,] map; // 맵의 구조
    // S:시작점, G:도착점, L:사다리, W:벽, E:길
    // 짝수:비활성화 길, 홀수:비활성화 사다리
    void Start()
    {
        
    }
/*
    9,3 / 0,1
    {W, W, W, W, L, E, L, W, G, 
    S, L, W, L, L, W, L, W, L, 
    W, L, E, L, W, E, L, E, L}
    
    */
}
