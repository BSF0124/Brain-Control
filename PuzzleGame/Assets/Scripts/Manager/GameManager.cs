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
        if(PauseMenu.isGamePaused)
        { 
            return;
        }

        if(SceneManager.GetActiveScene().name != "MainMenu" && SceneManager.GetActiveScene().name != "CutScene" && SceneManager.GetActiveScene().name != "World")
        {
            // 메인과 서브 캐릭터 모두 클리어 확인
            if(isMainClear && isSubClear && !isGameClear)
            {
                isGameClear = true;
                AudioManager.instance.PlaySfx(AudioManager.Sfx.StageClear);
                FadeManager.instance.FadeImage(0, 0.9f);
                FadeManager.instance.FadeText(0, 1);
                FadeManager.instance.FadeLoop();
                DataManager.instance.currentPlayer.isClear[DataManager.instance.currentPlayer.stageIndex] = true;
                DataManager.instance.SaveData();
            }
            if(!isGameClear && Input.GetKeyDown(KeyCode.R))
            {
                StartCoroutine(Restart());
            }
        }
    }

    public void ResetBool()
    {
        isMainClear = false;
        isSubClear = false;
        isGameClear = false;
        isSceneMove = false;
    }

    IEnumerator Restart()
    {
        ResetBool();
        Skeleton.animationSkipped = false;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.StageRestart);
        FadeManager.instance.FadeImage(0, 1);
        yield return null;
        // yield return new WaitForSeconds(FadeManager.instance.textDuration);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
