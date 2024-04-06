using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapManager : MonoBehaviour
{
    public GameObject[] tilemaps;
    private int stageIndex = 12;
    void Start()
    {
        for(int i = 0; i < 6; i++)
        {
            if(DataManager.instance.currentPlayer.isClear[stageIndex])
            {
                tilemaps[i].SetActive(true);
                stageIndex++;
            }

            else
            {
                return;
            }
        }
    }
}
