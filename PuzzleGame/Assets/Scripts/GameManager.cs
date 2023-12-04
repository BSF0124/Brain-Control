using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private FadeEffect fadeEffect;
    
    [HideInInspector]
    public bool main_Clear=false, sub_Clear=false, isGameClear=false, isSceneMove=false;
    public string sceneName;

    void Start()
    {
        fadeEffect = GameObject.Find("FadeImage").GetComponent<FadeEffect>();
        fadeEffect.FadeOut();

    }

    void Update()
    {
        // 메인과 서브 캐릭터 모두 클리어 확인
        if(main_Clear && sub_Clear)
        {
            isGameClear = true;
        }

        if(isGameClear && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            isSceneMove = true;
            MoveScene();
        }

        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(0);
    }
    
    // 씬 이동 코루틴 호출
    public void MoveScene() {
        StartCoroutine(Transition());
    }

    // 페이드 아웃 -> 씬 이동
    private IEnumerator Transition()
    {
        yield return new WaitForSeconds(fadeEffect.fadeTime);
        SceneManager.LoadScene(sceneName);
    }
}
