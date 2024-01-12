using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    private string currentStage;
    void Awake()
    {
        StageActive sa = GameObject.Find(currentStage).GetComponent<StageActive>();
        sa.Active();
    }
}
