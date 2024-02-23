using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    public Image fadeImage; // fade 이미지
    public TextMeshProUGUI clearText; // clear 텍스트
    public TextMeshProUGUI enterText; // enter 텍스트
    public float imageDuration = 0.2f; // 이미지 fade 시간
    public float textDuration = 0.2f; // 텍스트 fade 시간

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }

        // FadeImage(1,0,false);
    }

    // 이미지 fade 효과
    public void FadeImage(float start, float end, bool isActive)
    {
        if(isActive)
        {
            StartCoroutine(Fade_1(fadeImage, imageDuration, start, end));
        }

        else
        {
            StartCoroutine(Fade_0(fadeImage, imageDuration, start, end));
        }
    }

    // 텍스트 fade 효과
    public void FadeText(float start, float end, bool isActive)
    {
        if(isActive)
        {
            StartCoroutine(Fade_1(clearText, textDuration, start, end));
        }

        else
        {
            StartCoroutine(Fade_0(clearText, textDuration, start, end));
        }
    }

    // fade후 오브젝트 비활성화
    private IEnumerator Fade_0(Graphic graphic, float duration, float start, float end)
    {
        if(!graphic.gameObject.activeSelf)
        {
            graphic.gameObject.SetActive(true);
        }
        
        Color temp = graphic.color;
        temp.a = start;
        graphic.color = temp;

        graphic.DOFade(end, duration);

        yield return new WaitForSeconds(duration);

        graphic.gameObject.SetActive(false);
    }

    // fade후 오브젝트 활성화 유지
    private IEnumerator Fade_1(Graphic graphic, float duration, float start, float end)
    {
        if(!graphic.gameObject.activeSelf)
        {
            graphic.gameObject.SetActive(true);
        }
        
        Color temp = graphic.color;
        temp.a = start;
        graphic.color = temp;

        graphic.DOFade(end, duration);
        yield return new WaitForSeconds(duration);
    }

    // fade in, out 반복
    public void FadeLoop()
    {
        if(!enterText.gameObject.activeSelf)
        {
            enterText.gameObject.SetActive(true);
        }

        enterText.DOKill();

        enterText.DOFade(1, textDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                enterText.DOFade(0, textDuration)
                .SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    FadeLoop();
                });
            });
    }

    public void FadeLoopStop()
    {
        enterText.DOKill();
        enterText.DOFade(0, textDuration);
    }
}
