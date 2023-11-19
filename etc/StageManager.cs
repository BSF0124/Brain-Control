using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public TextAsset stageData;
    private AllData datas;

    void Awake()
    {
        datas = JsonUtility.FromJson<AllData>(stageData.text);
        print(datas.Stage[0].type);
    }
}

[System.Serializable]
public class MapData
{
    public int stageID;
    public string stageName;
    public int column;
    public int row;
    public int[,] type;
}

[System.Serializable]
public class AllData
{
    public MapData[] Stage;
}

