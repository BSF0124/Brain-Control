using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextFadeOut : MonoBehaviour
{
    private TextMeshProUGUI text;
    private GameManager gameManager;
    private bool gameClear = false;
    private bool sceneMove = false;
    public float fadeTime = 1;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        text = GameObject.Find("ClearText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(gameManager.isGameClear && !gameClear)
        {
            gameClear = true;
            FadeIn();
        }

        if(gameManager.isSceneMove && !sceneMove)
        {
            sceneMove =true;
            FadeOut();
        }
    }

    void FadeIn()
    {
        text.DOFade(1, fadeTime);
    }

    void FadeOut()
    {
        text.DOFade(0, fadeTime);
    }
}
