using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            animator.Play("Left 1", -1, 0f);
            animator.SetBool("left", true);
        }
    }

    void SetDefault()
    {
        animator.SetBool("left", false);
    }
}
