using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextFadeLoop : MonoBehaviour
{
    private TextMeshProUGUI text;
    private GameManager gameManager;
    private bool gameClear = false;
    private bool sceneMove = false;
    public float fadeTime = 1;
    
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        text = GameObject.Find("EnterText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(gameManager.isGameClear && !gameClear)
        {
            gameClear = true;
            Invoke("FadeInOut", 0.5f);
        }
        
        if(gameManager.isSceneMove && !sceneMove)
        {
            sceneMove = true;
            FadeOut();
        }
    }

    void FadeInOut()
    {
        text.DOKill();

        text.DOFade(1, fadeTime)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                text.DOFade(0, fadeTime)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        FadeInOut();
                    });
            });
    }

    void FadeOut()
    {
        text.DOKill();
        text.DOFade(0, fadeTime);
    }
}
