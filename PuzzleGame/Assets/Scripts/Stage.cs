using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Stage : MonoBehaviour
{
    public GameObject[] road;
    public float moveTime = 1;

    public void MoveLeft(Transform player) 
    {
        for(int i=road.Length-1; i>=0; i--)
        {
            Vector3 target = road[i].transform.position;
            StartCoroutine(Move(player, target));
        }
    }

    public void MoveRight(Transform player)
    {
        for(int i=0; i<road.Length; i++)
        {
            Vector3 target = road[i].transform.position;
            StartCoroutine(Move(player, target));
        }
    }

    private IEnumerator Move(Transform start, Vector3 end)
    {
        start.DOMove(end, moveTime);
        yield return new WaitForSeconds(moveTime);
    }
}
