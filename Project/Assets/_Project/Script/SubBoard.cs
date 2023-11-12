using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubBoard : MonoBehaviour
{
    private SpriteRenderer defaultImage;
    public Sprite ActivatedImage;

    // Start is called before the first frame update
    void Start()
    {
        defaultImage = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 충돌 감지
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "MainCharacter")
            Activated();
        Debug.Log("dd");
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
