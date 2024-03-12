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
    public float targetPostiion = 1.6f;
    public float animationDuration = 0.25f; // 애니메이션의 총 시간

    void Start()
    {
        StartCoroutine(ObjectMovement());
    }

    IEnumerator ObjectMovement()
    {
        GameManager.instance.isSceneMove = true;
        float elapsedTime = 0;
        yield return new WaitForSeconds(0.5f);

        while (skel2.transform.localScale.x < targetScale)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration); // 현재 진행된 시간의 비율 (0~1)
            float curvedStep = Mathf.Lerp(0, targetScale, Mathf.Pow(t, 2)); // 곡선 운동을 위해 진행률을 제곱하여 사용

            float step = curvedStep * Time.deltaTime;

            skel1.transform.position -= new Vector3(targetPostiion * step, 0, 0);
            skel2.transform.localScale += new Vector3(step, 0, 0);
            skel3.transform.localScale += new Vector3(step, 0, 0);
            skel4.transform.position += new Vector3(targetPostiion * step, 0, 0);

            yield return null;
        }

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

        GameManager.instance.isSceneMove = false;
    }
}
