using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector]
    public bool isMainClear=false; // 메인 캐릭터 도착 지점 도달

    [HideInInspector]
    public bool isSubClear=false; // 서브 캐릭터 도착 지점 도달

    [HideInInspector]
    public bool isGameClear=false; // 메인과 서브 캐릭터 모두 도착 지점 도달

    [HideInInspector]
    public bool isSceneMove=false; // 씬이 이동중임

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // 메인과 서브 캐릭터 모두 클리어 확인
        if(isMainClear && isSubClear && !isGameClear)
        {
            isGameClear = true;
            FadeManager.instance.FadeImage(0, 0.9f, true);
            FadeManager.instance.FadeText(0, 1, true);
            FadeManager.instance.FadeLoop();
            DataManager.instance.currentPlayer.isClear[DataManager.instance.currentPlayer.stageIndex] = true;
            DataManager.instance.SaveData();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetBool();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene(0);
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ResetBool()
    {
        isMainClear = false;
        isSubClear = false;
        isGameClear = false;
        isSceneMove = false;
    }
}
