using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearCheck : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        string[] str = gameObject.name.Split();
        int index = int.Parse(str[1]) - 1;
        
        if(DataManager.instance.currentPlayer.isClear[index])
        {
            animator.SetBool("Clear", true);
        }

        // else
        // {
        //     animator.SetBool("Clear", false);
        // }
    }
}
