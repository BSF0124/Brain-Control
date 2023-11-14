using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubCharacter : MonoBehaviour
{
    private Vector3 targetPosition;

    public float char_X, char_Y; // 캐릭터 초기 좌표
    public float sum_X, sum_Y; // 캐릭터 상하좌우 이동 거리
    public float min_X, min_Y; // 최소 좌표
    public float max_X, max_Y; // 최대 좌표
    private bool ladder_up = false; // 올라갈 수 있는지 확인
    private bool ladder_down = false; // 내려갈 수 있는지 확인

    GameManager gameManager;

    void Awake()
    {
        SetCharacter();
        targetPosition = transform.localPosition;
    }

    void SetCharacter()
    {
        transform.localPosition = new Vector3(char_X, char_Y, 0);
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
        if(other.gameObject.tag == "Goal")
            Debug.Log("Success");
        
        if(other.gameObject.tag == "ladder_Up")
            ladder_up = true;

        if(other.gameObject.tag == "ladder_Down")
            ladder_down = true;
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.tag == "ladder_Up")
            ladder_up = false;

        if(other.gameObject.tag == "ladder_Down")
            ladder_down = false;
    }
}
