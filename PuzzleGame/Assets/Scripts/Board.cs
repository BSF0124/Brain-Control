using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private SpriteRenderer defaultImage;
    public Sprite ActivatedImage;

    // 발판 활성화
    public void Activated() 
    {
        // defaultImage.sprite = ActivatedImage;
        // gameObject.tag = "ActivatedBoard";
    }
}
