using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubCharacter : MonoBehaviour
{
    private Vector3 targetPosition;

    public float sum_X, sum_Y; // 캐릭터 상하좌우 이동 거리
    private bool ladder_up = false; // 올라갈 수 있는지 확인
    private bool ladder_down = false; // 내려갈 수 있는지 확인

    GameManager GameManager;

    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetPosition = transform.localPosition;
    }

    public void Move_Left()
    {
        targetPosition -= new Vector3(sum_X, 0, 0);
        transform.DOLocalMove(targetPosition, 0.25f);

    }

    public void Move_Right()
    {
        targetPosition += new Vector3(sum_X, 0, 0);
        transform.DOLocalMove(targetPosition, 0.25f);
    }

    // 캐릭터가 사다리 오브젝트와 충돌되어있으면 올라감/내려감
    public void Move_Up()
    {
        if(ladder_up)
        {
            targetPosition += new Vector3(0, sum_Y, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
        }
    }

    public void Move_Down()
    {
        if(ladder_down)
        {
            targetPosition -= new Vector3(0, sum_Y, 0);
            transform.DOLocalMove(targetPosition, 0.25f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "ladder_Up")
            ladder_up = true;

        if(other.gameObject.tag == "ladder_Down")
            ladder_down = true;
        
        if(other.gameObject.tag == "Goal")
            GameManager.sub_Clear = true;
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "ladder_Up")
            ladder_up = false;

        if(other.gameObject.tag == "ladder_Down")
            ladder_down = false;
        
        if(other.gameObject.tag == "Goal")
            GameManager.sub_Clear = false;
    }
}
