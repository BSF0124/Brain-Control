using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Numerics;

public class Stage : MonoBehaviour
{
    // public GameObject[] road;
    public LineRenderer line;
    public float moveTime = 1;
    private bool isMoving = false;

    public void MoveLeft(Transform cur_transform) 
    {
        if(!isMoving)
        {
        StartCoroutine(DoMove(cur_transform, true));

        }

    }

    public void MoveRight(Transform cur_transform)
    {
        if(!isMoving)
        {
        StartCoroutine(DoMove(cur_transform, false));
        }

    }

    private IEnumerator DoMove(Transform cur_transform, bool moveLeft)
    {
        isMoving = true;
        UnityEngine.Vector3[] pathPoints = new UnityEngine.Vector3[line.positionCount];
        line.GetPositions(pathPoints);

        if(moveLeft)
        {
            for(int i=pathPoints.Length-2; i>=0; i++)
            {
                UnityEngine.Vector3 target = pathPoints[i];
                yield return StartCoroutine(Move(cur_transform, target));
            }
        }
        else
        {
            for(int i=1; i<pathPoints.Length; i++)
            {
                UnityEngine.Vector3 target = pathPoints[i];
                yield return StartCoroutine(Move(cur_transform, target));
            }
        }

        isMoving = false;
    }

    private IEnumerator Move(Transform start, UnityEngine.Vector3 end)
    {
        start.DOMove(end, moveTime).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(moveTime);
    }


    /*
    private IEnumerator MoveAlongPath(Transform player, bool moveLeft)
    {
        if(moveLeft)
        {
            for(int i=road.Length-2; i>=0; i--)
            {
                Vector3 target = road[i].transform.position;
                yield return StartCoroutine(Move(player, target));
            }
        }

        else
        {
            for(int i=1; i<road.Length; i++)
            {
                Vector3 target = road[i].transform.position;
                yield return StartCoroutine(Move(player, target));
            }
        }
    }

    private IEnumerator Move(Transform start, Vector3 end)
    {
        start.DOMove(end, moveTime).SetEase(Ease.InOutSine);
        yield return new WaitForSeconds(waitTime);
    }
    */
}
