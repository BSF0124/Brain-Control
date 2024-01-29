using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageActive : MonoBehaviour
{
    public bool isActive = false;
    void Start()
    {
        gameObject.SetActive(isActive);
    }
}
