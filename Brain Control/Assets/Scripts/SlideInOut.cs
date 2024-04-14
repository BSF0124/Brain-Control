using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInOut : MonoBehaviour
{
    public Animator animator;

    public IEnumerator SlideIn()
    {
        gameObject.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.MoveWorld);
        animator.Play("SlideIn");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("SlideIn") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
    }

    public IEnumerator SlideOut()
    {
        animator.Play("SlideOut");
        AudioManager.instance.PlaySfx(AudioManager.Sfx.MoveWorld);
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("SlideOut") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
        gameObject.SetActive(false);
    }
}
