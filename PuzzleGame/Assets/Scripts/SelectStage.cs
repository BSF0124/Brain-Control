using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SelectStage : MonoBehaviour
{
    public List<Transform> stages;
    private int stageIndex = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(stageIndex > 0)
            {
                stageIndex--;
                MoveToStage(stageIndex);
            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(stageIndex < stages.Count - 1)
            {
                stageIndex++;
                MoveToStage(stageIndex);
            }
        }
    }

    void MoveToStage(int index) 
    {
        Vector3 endPosition = stages[index].position;

        transform.DOMove(endPosition, 1f);
    }
}
