using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubBoard : MonoBehaviour
{
    private SpriteRenderer defaultImage;
    public Sprite ActivatedImage;

    SubCharacter SubCharacter;

    // Start is called before the first frame update
    void Start()
    {
        defaultImage = GetComponent<SpriteRenderer>();
        SubCharacter = GameObject.Find("SubCharacter").GetComponent<SubCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(this.gameObject.tag == "LeftBoard")
            SubCharacter.Move_Left();
        else if(this.gameObject.tag == "RightBoard")
            SubCharacter.Move_Right();
        else if(this.gameObject.tag == "UpBoard")
            SubCharacter.Move_Up();
        else if(this.gameObject.tag == "DownBoard")
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
        gameObject.tag = "ActivatedBoard";
    }

}
