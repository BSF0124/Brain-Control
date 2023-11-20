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
        print(main_Clear + " " + sub_Clear);
    }

    // Update is called once per frame
    void Update()
    {
        if(main_Clear && sub_Clear)
        {
            GameClear();
        }
    }

    public void GameClear()
    {

    }
}
