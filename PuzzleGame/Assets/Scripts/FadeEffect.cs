using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FadeEffect : MonoBehaviour
{
    private Image image; //페이드 효과에 사용되는 검은 바탕 이미지
    private GameManager gameManager;
    private bool gameClear = false;
    private bool sceneMove = false;
    public float fadeTime = 1;

    void Awake()
    {
        image = GetComponent<Image>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(gameManager.isGameClear && !gameClear)
        {
            gameClear = true;
            StartCoroutine(Fade(0, 0.9f));
        }

        if(gameManager.isSceneMove && !sceneMove)
        {
            sceneMove = true;
            StartCoroutine(Fade(0.9f, 1));
        }
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(1, 0));
    }

    private IEnumerator Fade(float start, float end)
    {
        Color temp = image.color;
        temp.a = start;
        image.color = temp;

        image.DOFade(end, fadeTime);
        yield return new WaitForSeconds(fadeTime);
    }
}
