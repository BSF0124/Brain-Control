using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearCheck : MonoBehaviour
{
    void Start()
    {
        string[] str = gameObject.name.Split();
        int index = int.Parse(str[1]) - 1;

        SpriteRenderer sprite =  gameObject.GetComponent<SpriteRenderer>();
        if(DataManager.instance.currentPlayer.isClear[index])
        {
            sprite.color = new Color(0/255f, 200/255f, 255/255f);
        }

        else
        {
            sprite.color = Color.white;
        }
    }
}
