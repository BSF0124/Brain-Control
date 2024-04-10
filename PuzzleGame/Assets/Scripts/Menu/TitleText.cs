using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TitleText : MonoBehaviour
{
    public float duration = 1f;
    TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Increase());
    }

    IEnumerator Increase()
    {
        float elapsedTime = 0;
        while(text.fontSize < 150)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float curvedStep = Mathf.Lerp(0, 150, Mathf.Pow(t, 2));
            float step = curvedStep * Time.deltaTime;
            text.fontSize += step;
            yield return null;
        }
        StartCoroutine(Reduce());
    }

    IEnumerator Reduce()
    {
        float elapsedTime = 0;
        while(text.fontSize > 100)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            float curvedStep = Mathf.Lerp(0, 100, Mathf.Pow(t, 2));
            float step = curvedStep * Time.deltaTime;
            text.fontSize -= step;
            yield return null;
        }
        StartCoroutine(Increase());
    }
}
