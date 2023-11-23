using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public FadeEffect fadeEffect;
    public string sceneName;

    void Start()
    {
        if(fadeEffect == null)
            Debug.LogError("SceneMove 스크립트에 FadeEffect 참조가 설정되지 않았습니다.");
        
        fadeEffect.FadeIn();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
            MoveScene();
    }

    public void MoveScene() {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        fadeEffect.FadeOut();

        yield return new WaitForSeconds(fadeEffect.fadeTime);

        SceneManager.LoadScene(sceneName);
    }
}
