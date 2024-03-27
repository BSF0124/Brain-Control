using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public int stageIndex = 0;

    // true : 스테이지 클리어
    public bool[] isClear = new bool[29]
    {
        false,false,false,false,false,false,false,false,
        false,false,false,false,false,false,
        false,false,false,false,false,false,
        false,false,false,false,false,false,
        false,false,false
    };
}
[System.Serializable]
public class StageData
{
    public string stageID;
    public int map_Width;
    public int map_Height;
    public int map_X;
    public int map_Y;
    public char[] map_Elements;

    public int board_Width;
    public int board_Height;
    public int board_X;
    public int board_Y;
    public char[] board_Elements;
}
[System.Serializable]
public class StageList
{
    public StageData[] stage;
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData currentPlayer = new PlayerData(); // 현재 플레이어 데이터
    public TextAsset datafile;
    [HideInInspector]
    public string path; // 경로
    [HideInInspector]
    public int currentSlot; // 사용 슬롯 번호
    public StageList stageList;

    private void Awake()
    {
        // 싱글톤
        if(instance == null)
        {
            instance = this;
        }

        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);

        // 경로 설정
        path = Application.persistentDataPath;

        stageList = JsonUtility.FromJson<StageList>(datafile.text);
    }

    // 데이터 저장
    public void SaveData()
    {
        // 데이터를 Json 형태로 변환
        string data = JsonUtility.ToJson(currentPlayer, true);

        // 데이터 저장
        File.WriteAllText($"{path}/save{currentSlot}.json", data);
    }

    // 데이터 불러오기
    public void LoadData()
    {
        // 경로에 있는 Json 데이터를 읽어옴
        string data = File.ReadAllText($"{path}/save{currentSlot}.json");

        // 데이터 불러옴
        currentPlayer = JsonUtility.FromJson<PlayerData>(data);
    }

    // 초기화
    public void DataClear()
    {
        currentSlot = -1;
        currentPlayer = new PlayerData();
    }

    // 데이터 삭제
    public void DeleteData()
    {
        File.Delete($"{path}/save{currentSlot}.json");
    }

    public bool StageClearCheck(int stage)
    {
        return currentPlayer.isClear[stage];
    }
}
