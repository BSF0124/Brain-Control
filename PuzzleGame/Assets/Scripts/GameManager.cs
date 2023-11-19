using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool main_Clear = false;
    public bool sub_Clear = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Success()
    {
        if(main_Clear && sub_Clear)
        {
            Debug.Log("Clear!");
        }
    }
}
