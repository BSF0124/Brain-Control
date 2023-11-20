using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private SpriteRenderer defaultImage;
    public Sprite ActivatedImage;

    SubCharacter SubCharacter;

    void Start()
    {
        defaultImage = GetComponent<SpriteRenderer>();
        SubCharacter = GameObject.Find("SubCharacter").GetComponent<SubCharacter>();
    }

    // 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.tag == "LeftBoard")
            SubCharacter.Move_Left();
        else if(gameObject.tag == "RightBoard")
            SubCharacter.Move_Right();
        else if(gameObject.tag == "UpBoard")
            SubCharacter.Move_Up();
        else if(gameObject.tag == "DownBoard")
            SubCharacter.Move_Down();

        if(other.gameObject.tag == "MainCharacter")
            Activated();
    }

    // 발판 활성화
    public void Activated() 
    {
        // 이미지 변경
        defaultImage.sprite = ActivatedImage;

        // 태그 변경
        if(gameObject.tag != "EndBoard")
            gameObject.tag = "ActivatedBoard";
    }
}
