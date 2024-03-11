using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public GameObject skel1;
    public GameObject skel2;
    public GameObject skel3;
    public GameObject skel4;
    public float targetScale = 2;
    public float speed = 1;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(ObjectMovement());
        }
    }

    IEnumerator ObjectMovement()
    {
        GameManager.instance.isGameClear = true;

        float elapsedTime = 0;
        float duration = 1;

        // ObjectMovement 코루틴 실행
        while (skel2.transform.localScale.x < targetScale)
        {
            float t = elapsedTime / duration;
            float easedT = EaseInOutCurve(t);
            float step = speed * Time.deltaTime * easedT;

            skel1.transform.position -= new Vector3(step, 0, 0);
            skel2.transform.localScale += new Vector3(step, 0, 0);
            skel3.transform.localScale += new Vector3(step, 0, 0);
            skel4.transform.position += new Vector3(step, 0, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ObjectSeparate 코루틴 실행
        yield return StartCoroutine(ObjectSeparate());
    }


    IEnumerator ObjectSeparate()
    {
        float randomRotation = Random.Range(-30f, 30f);
        skel3.transform.rotation = Quaternion.Euler(0, 0, randomRotation);

        Rigidbody2D rigidBody = skel3.GetComponent<Rigidbody2D>();
        rigidBody.gravityScale = 3;
        float randomJumpForce = Random.Range(4f,8f);
        Vector3 jumpVelocity = Vector3.up * randomJumpForce;
        rigidBody.AddForce(jumpVelocity, ForceMode2D.Impulse);

        // 점프 후 아래로 떨어질 때까지 대기
        while (skel3.transform.position.y > -10)
        {
            yield return null;
        }
        Destroy(skel3);

        GameManager.instance.isGameClear = false;
    }

    float EaseInOutCurve(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
