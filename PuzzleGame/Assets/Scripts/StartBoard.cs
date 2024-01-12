using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoard : MonoBehaviour
{
    // 생성할 메인 캐릭터
    public GameObject character;
    public void CreateMain()
    {
        // 메인 캐릭터 생성
        GameObject mainCharacter = Instantiate(character);

        // 메인 캐릭터의 부모 설정
        mainCharacter.transform.parent = transform.parent;

        // 메인 캐릭터의 좌표 설정
        mainCharacter.transform.localPosition = transform.localPosition;
    }
}
