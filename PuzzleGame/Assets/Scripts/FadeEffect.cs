using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
    [Range(0.01f, 10f)]
    public float fadeTime; // fadeSpeed 값이 10이면 1초 (값이 클수록 빠름)

    [SerializeField]
    private AnimationCurve fadeCurve; // 페이드 효과가 적용되는 값을 곡선의 값으로 설정
    private Image image; //페이드 효과에 사용되는 검은 바탕 이미지

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(1, 0));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(0, 1));
    }

    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while(percent < 1)
        {
            // fadeTime으로 나누어서 fadeTime 시간 동안
            // percent 값이 0에서 1로 증가하도록 함
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            // 알파값을 start부터 end까지 fadeTime 시간 동안 변화시킨다
            Color color = image.color;

            // color.a = Mathf.Lerp(start, end, percent);
            color.a = Mathf.Lerp(start, end, fadeCurve.Evaluate(percent));
            image.color = color;
            yield return null;
        }
    }
}
