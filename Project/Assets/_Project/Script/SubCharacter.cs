using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SubCharacter : MonoBehaviour
{
    private Vector3 targetPosition;

    public float sum_X; // 좌우 이동 거리
    public float sum_Y; // 상하 이동 거리
    private bool ladder_up = false;
    private bool ladder_down = false;

    GameManager gameManager;


    void Start()
    {
        targetPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

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

    // 캐릭터가 사다리 오브젝트와 충돌되어있으면 올라감,내려감
    public void Move_Up()
    {
        targetPosition += new Vector3(0, sum_Y, 0);
        transform.DOLocalMove(targetPosition, 0.25f);
    }

    public void Move_Down()
    {
        targetPosition -= new Vector3(0, sum_Y, 0);
        transform.DOLocalMove(targetPosition, 0.25f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Goal")
            Debug.Log("Success");
        
        if(other.gameObject.tag == "ladder_up")
            ladder_up = true;

        if(other.gameObject.tag == "ladder_down")
            ladder_down = true;
    }

}
