using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInOut : MonoBehaviour
{
    public Animator animator;

    public IEnumerator SlideIn()
    {
        animator.Play("SlideIn");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("SlideIn") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
    }

    public IEnumerator SlideOut()
    {
        animator.Play("SlideOut");
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("SlideOut") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f);
    }
}
