using UnityEngine;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    public Animator animator;

    public void WipeIn()
    {
        animator.Play("SlideIn");
    }

    public void WipeOut()
    {
        animator.Play("SlideOut");
    }
}
