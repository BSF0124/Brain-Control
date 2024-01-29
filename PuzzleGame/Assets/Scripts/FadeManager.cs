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

        FadeOutImage();
    }

    public void FadeInImage()
    {
        StartCoroutine(FadeIn(fadeImage, imageDuration));
    }

    public void FadeOutImage()
    {
        StartCoroutine(FadeOut(fadeImage, imageDuration));
    }

    public void FadeInText(TextMeshProUGUI text)
    {
        StartCoroutine(FadeIn(text, textDuration));
    }

    public void FadeOutText(TextMeshProUGUI text)
    {
        StartCoroutine(FadeOut(text, textDuration));
    }

    public void FadeLoop()
    {
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

    private IEnumerator FadeIn(Graphic graphic, float duration)
    {
        graphic.gameObject.SetActive(true);
        Color temp = graphic.color;
        temp.a = 0;
        graphic.color = temp;

        graphic.DOFade(1, duration);
        yield return new WaitForSeconds(duration);
    }

    private IEnumerator FadeOut(Graphic graphic, float duration)
    {
        if(!graphic.gameObject.activeSelf)
        {
            graphic.gameObject.SetActive(true);
        }
        
        Color temp = graphic.color;
        temp.a = 1;
        graphic.color = temp;

        graphic.DOFade(0, duration);
        yield return new WaitForSeconds(duration);

        graphic.gameObject.SetActive(false);
    }
}
