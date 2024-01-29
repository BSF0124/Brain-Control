using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerMove : MonoBehaviour
{
    public SlideInOut slideInOut;
    public Camera mainCamera; // 메인 카메라
    private WorldManager worldManager;

    public float moveSpeed; // 플레이어 이동 속도
    public int maxWorldIndex; // 최대 월드 수
    private int maxStageIndex; // 현재 월드에 해당하는 최대 스테이지 수
    private int worldIndex; // 현재 월드 인덱스
    private int stageIndex; // 현재 스테이지 인덱스
    private bool isMoving = false; // 플레이어가 이동 중인지 확인
    private bool onStage = false;

    void Start()
    {
        worldIndex = DataManager.instance.currentPlayer.currentWorld;
        stageIndex = DataManager.instance.currentPlayer.currentStage;
        SetWorld();
        mainCamera.transform.position = new Vector3(worldIndex*20, 0, -5);
        Vector3 temp = GameObject.Find("World " + (worldIndex+1)).GetComponent<WorldManager>().stage[stageIndex].position;
        transform.position = temp;
    }

    void Update()
    {
        // 플레이어가 이동중이면 키 입력 불가
        if(isMoving)
            return;

        HandleInput();
    }

    // 키보드 입력 처리
    private void HandleInput()
    {
        // 이전 스테이지로 이동
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 이전 스테이지로 이동
            if(stageIndex > 0)
            {
                // 스테이지 이동
                stageIndex--;
                StartCoroutine(MoveStage());
            }

            // 월드의 첫 번째 스테이지이면 이전 월드로 이동(첫 번째 월드는 제외)
            else if(worldIndex > 0)
            {
                // 월드 이동
                worldIndex--;
                MoveWorld();
            }
        }

        // 다음 스테이지로 이동
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 현재 스테이지를 클리어 했는지 확인
            if(!DataManager.instance.StageClearCheck(worldIndex,stageIndex))
            {
                return;
            }
            
            // 다음 스테이지로 이동
            if(stageIndex < maxStageIndex - 1)
            {
                // 다음 스테이지를 클리어 하지 못했으면 이동하지 않음
                // if(!DataManager.instance.StageClearCheck(worldIndex-1,stageIndex+1))
                // {
                //     return;
                // }

                // 스테이지 이동
                stageIndex++;
                StartCoroutine(MoveStage());
            }

            // 월드의 마지막 스테이지이면 다음 월드로 이동(마지막 월드는 제외)
            else if(worldIndex < maxWorldIndex)
            {
                // 다음 스테이지를 클리어 하지 못했으면 이동하지 않음
                // if(!DataManager.instance.StageClearCheck(worldIndex+1,0))
                // {
                //     return;
                // }

                //월드 이동
                worldIndex++;
                MoveWorld();
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(onStage)
            {
                StartCoroutine(GoStage());
            }
        }
    }

    // 스테이지 이동 코루틴
    public IEnumerator MoveStage()
    {
        StartMoving();

        Vector3 stagePosition = worldManager.stage[stageIndex].transform.position;
        Tweener tweener = transform.DOMove(stagePosition, moveSpeed).OnComplete(StopMoving);
        yield return tweener.WaitForCompletion();
    }

    // 월드 이동 메서드
    public void MoveWorld()
    {
        StartMoving();
        StartCoroutine(MoveWorldRoutine());
    }

    // 월드 이동 코루틴
    private IEnumerator MoveWorldRoutine()
    {
        SetWorld();

        if(stageIndex == 0)
            stageIndex = maxStageIndex - 1;
        else
            stageIndex = 0;

        yield return slideInOut.SlideIn();

        Vector3 worldPosition = new Vector3(worldIndex * 20, mainCamera.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = worldPosition;

        yield return slideInOut.SlideOut();

        yield return MoveStage(); // 플레이어 이동
        
        StopMoving();
    }

    // 월드와 스테이지 정보 불러오기
    private void SetWorld()
    {
        var world = GameObject.Find("World " + (worldIndex+1));
        worldManager = world.GetComponent<WorldManager>();
        maxStageIndex = worldManager.stage.Length;
    }

    private void StartMoving()
    {
        isMoving = true;
    }

    private void StopMoving()
    {
        isMoving = false;
    }

    // 충돌 감지
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Stage")
        {
            Save();
            onStage = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Stage")
        {
            onStage = false;
        }
    }

    // 데이터 저장
    private void Save()
    {
        DataManager.instance.currentPlayer.currentWorld = worldIndex;
        DataManager.instance.currentPlayer.currentStage = stageIndex;
        DataManager.instance.SaveData();
    }

    // 스테이지 씬 이동
    private IEnumerator GoStage()
    {
        FadeManager.instance.FadeImage(0, 1, true);
        yield return new WaitForSeconds(FadeManager.instance.imageDuration);
        SceneManager.LoadScene("Stage " + (worldIndex+1));
    }
}
