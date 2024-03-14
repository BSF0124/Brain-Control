using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBoard : MonoBehaviour
{
    // 생성할 메인 캐릭터
    public GameObject character;
    private GameObject mainCharacter;
    public void Start()
    {
        if(Skeleton.animationSkipped)
        {
            CreateCharacter();
        }
        else
        {
            Invoke("CreateCharacter", 1f);
        }
    }

    void CreateCharacter()
    {
        // 메인 캐릭터 생성
        mainCharacter = Instantiate(character);
        mainCharacter.transform.localScale = new Vector3(0,0,0);
        // 메인 캐릭터의 부모 설정
        mainCharacter.transform.parent = transform.parent;

        // 메인 캐릭터의 좌표 설정
        mainCharacter.transform.localPosition = transform.localPosition + new Vector3(0, 0, -0.5f);
        if(Skeleton.animationSkipped)
        {
            mainCharacter.transform.localScale = new Vector3(1,1,1);
            GameManager.instance.isSceneMove = false;
        }

        else
        {
            StartCoroutine(CharacterScaleUp());
        }
    }

    IEnumerator CharacterScaleUp()
    {
        while(mainCharacter.transform.localScale.x < 1)
        {
            mainCharacter.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            yield return null;
        }
        GameManager.instance.isSceneMove = false;
    }
}
