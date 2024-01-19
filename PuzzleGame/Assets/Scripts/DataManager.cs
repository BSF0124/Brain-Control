using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlayerData
{
    public int difficulty = 0; // 0 : 쉬움, 1 : 보통, 2 : 어려움
    public string currentStage = "Stage 1";

    // true : 스테이지 클리어
    public bool[] isClear = new bool[19]{
        true,false,false,false,false,false,false,
        false,false,false,false,false,false,
        false,false,false,false,false,false};
}

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public PlayerData currentPlayer = new PlayerData(); // 현재 플레이어 데이터
    public string path; // 경로
    public int currentSlot; // 사용 슬롯 번호

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
        DontDestroyOnLoad(this.gameObject);

        // 경로 설정
        path = Application.persistentDataPath + "/save";
    }

    // 데이터 저장
    public void SaveData()
    {
        // 데이터를 Json 형태로 변환
        string data = JsonUtility.ToJson(currentPlayer);

        // 데이터 저장
        File.WriteAllText(path + currentSlot.ToString(), data);
    }

    // 데이터 불러오기
    public void LoadData()
    {
        // 경로에 있는 Json 데이터를 읽어옴
        string data = File.ReadAllText(path + currentSlot.ToString());

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
        File.Delete(path + currentSlot.ToString());
    }
}
