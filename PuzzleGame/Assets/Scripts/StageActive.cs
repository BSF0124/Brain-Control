using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageActive : MonoBehaviour
{
    public bool isActive = false;

    void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }
}
