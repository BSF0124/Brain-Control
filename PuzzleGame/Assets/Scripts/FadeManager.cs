using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    public Image fadeImage;
    public TextMeshProUGUI clearText;
    public TextMeshProUGUI enterText;
    public float imageDuration = 1;
    public float textDuration = 1;

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
        DontDestroyOnLoad(gameObject);
    }

    public void ImageFadeIn()
    {
        StartCoroutine(ImageFade(0,1));
    }

    public void ImageFadeOut()
    {
        StartCoroutine(ImageFade(1,0));
    }

    private IEnumerator ImageFade(float start, float end)
    {
        Color temp = fadeImage.color;
        temp.a = start;
        fadeImage.color = temp;
        fadeImage.DOFade(end, imageDuration);
        yield return  new WaitForSeconds(imageDuration);
    }

    public void TextFadeIn(TextMeshProUGUI text)
    {
        StartCoroutine(TextFade(0,1,text));
    }

    public void TextFadeOut(TextMeshProUGUI text)
    {
        StartCoroutine(TextFade(1,0,text));
    }

    public void FadeLoop(TextMeshProUGUI text)
    {
        text.DOKill();
        text.DOFade(1, textDuration)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                text.DOFade(0, textDuration)
                    .SetEase(Ease.InOutQuad)
                    .OnComplete(() =>
                    {
                        FadeLoop(text);
                    });
            });
    }

    private IEnumerator TextFade(float start, float end, TextMeshProUGUI text)
    {
        Color temp = text.color;
        temp.a = start;
        text.color = temp;
        text.DOFade(end, textDuration);
        yield return  new WaitForSeconds(textDuration);
    }
}
