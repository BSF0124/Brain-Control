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
    public int maxWorld = 3; // 최대 월드 수
    private int[] maxStage = new int[3]{8,6,6}; // 현재 월드의 스테이지 수
    public static int currentWorld; // 현재 월드 인덱스
    public static int currentStage; // 현재 스테이지 인덱스
    private int stageIndex;
    private bool isMoving = false; // 플레이어가 이동 중인지 확인
    private bool onStage = false;

    void Start()
    {
        currentWorld = 0;
        stageIndex = DataManager.instance.currentPlayer.stageIndex;
        currentStage = stageIndex;

        for(int i=0; i<maxWorld; i++)
        {
            if(currentStage >= maxStage[i])
            {
                currentWorld++;
                currentStage -= maxStage[i];
            }
            else
            {
                break;
            }
        }
        SetWorld();
        mainCamera.transform.position = new Vector3(currentWorld*20, 0, -5);
        Vector3 temp = GameObject.Find("World " + (currentWorld+1)).GetComponent<WorldManager>().stage[currentStage].position;
        transform.position = temp;

        FadeManager.instance.FadeImage(1, 0, false);
    }

    void Update()
    {
        if(PauseMenu.isGamePaused)
        {
            return;
        }

        // 플레이어가 이동중이면 키 입력 불가
        if(isMoving)
        {
            return;
        }

        HandleInput();
    }

    // 키보드 입력 처리
    private void HandleInput()
    {
        // 이전 스테이지로 이동
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // 이전 스테이지로 이동
            if(currentStage > 0)
            {
                // 스테이지 이동
                currentStage--;
                stageIndex--;
                StartCoroutine(MoveStage());
            }

            // 월드의 첫 번째 스테이지이면 이전 월드로 이동(첫 번째 월드는 제외)
            else if(currentWorld > 0)
            {
                // 월드 이동
                currentWorld--;
                stageIndex--;
                StartCoroutine(MoveWorldRoutine());
            }
        }

        // 다음 스테이지로 이동
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 현재 스테이지를 클리어 했는지 확인
            if(!DataManager.instance.StageClearCheck(stageIndex))
            {
                return;
            }
            
            // 다음 스테이지로 이동
            if(currentStage < maxStage[currentWorld] - 1)
            {
                // 다음 스테이지를 클리어 하지 못했으면 이동하지 않음
                // if(!DataManager.instance.StageClearCheck(currentWorld-1,currentStage+1))
                // {
                //     return;
                // }

                // 스테이지 이동
                currentStage++;
                stageIndex++;
                StartCoroutine(MoveStage());
            }

            // 월드의 마지막 스테이지이면 다음 월드로 이동(마지막 월드는 제외)
            else if(currentWorld < maxWorld-1)
            {
                // 다음 스테이지를 클리어 하지 못했으면 이동하지 않음
                // if(!DataManager.instance.StageClearCheck(currentWorld+1,0))
                // {
                //     return;
                // }

                //월드 이동
                currentWorld++;
                stageIndex++;
                StartCoroutine(MoveWorldRoutine());
            }
        }

        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(onStage)
            {
                StartMoving();
                StartCoroutine(GoStage());
            }
        }
    }

    // 스테이지 이동 코루틴
    public IEnumerator MoveStage()
    {
        StartMoving();

        Vector3 stagePosition = worldManager.stage[currentStage].transform.position;
        Tweener tweener = transform.DOMove(stagePosition, moveSpeed).OnComplete(StopMoving);
        yield return tweener.WaitForCompletion();
    }


    // 월드 이동 코루틴
    private IEnumerator MoveWorldRoutine()
    {
        StartMoving();
        SetWorld();

        if(currentStage == 0)
            currentStage = maxStage[currentWorld] - 1;
        else
            currentStage = 0;

        yield return slideInOut.SlideIn();

        Vector3 worldPosition = new Vector3(currentWorld * 20, mainCamera.transform.position.y, mainCamera.transform.position.z);
        mainCamera.transform.position = worldPosition;

        yield return slideInOut.SlideOut();

        yield return MoveStage(); // 플레이어 이동
        
        StopMoving();
    }

    // 월드와 스테이지 정보 불러오기
    private void SetWorld()
    {
        var world = GameObject.Find("World " + (currentWorld+1));
        worldManager = world.GetComponent<WorldManager>();
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
        DataManager.instance.currentPlayer.stageIndex = stageIndex;
        DataManager.instance.SaveData();
    }

    // 스테이지 씬 이동
    private IEnumerator GoStage()
    {
        FadeManager.instance.FadeImage(0, 1, true);
        yield return new WaitForSeconds(FadeManager.instance.imageDuration);
        SceneManager.LoadScene("Stage " + (currentWorld+1));
    }
}
