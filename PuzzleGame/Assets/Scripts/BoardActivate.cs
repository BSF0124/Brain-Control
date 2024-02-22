using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardActivate : MonoBehaviour
{
    private SpriteRenderer image;
    public Sprite activationImage;

    void Start()
    {
        image = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Activate();
    }

    public void Activate() 
    {
        image.sprite = activationImage;
    }
}
