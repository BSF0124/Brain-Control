using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSubCharacter : MonoBehaviour
{
    // 생성할 서브 캐릭터
    public GameObject character;
    public void Start()
    {
        // 서브 캐릭터 생성
        GameObject subCharacter = Instantiate(character);

        // 서브 캐릭터의 부모 설정
        subCharacter.transform.parent = transform.parent;

        // 서브 캐릭터의 좌표 설정
        subCharacter.transform.localPosition = transform.localPosition;
    }
}
