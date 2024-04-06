using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip[] bgmClips;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Bgm {MainMenu, World, Stage}

    public enum Sfx {GotoStage, PlayerBlock=3, PlayerMove, PlayerSlip=6, PlayerTool,
    StageClear, StageMove=10, StageRestart}

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(instance.gameObject);
        }
        Init();
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("Bgm");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClips[0];
        bgmPlayer.Play();

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("Sfx");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int index=0; index<sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch(scene.name)
        {
            case "MainMenu":
                PlayBgm(Bgm.MainMenu);
                break;
            case "World":
                PlayBgm(Bgm.World);
                break;
            case "CutScene":
                StopBgm();
                break;
            default:
                PlayBgm(Bgm.Stage);
                break;
        }
    }

    public void PlayBgm(Bgm bgm)
    {
        if(bgmPlayer == null) return;
        
        int randIndex = 0;
        if(bgm == Bgm.Stage)
        {
            randIndex = Random.Range(0, bgmClips.Length-2);
        }

        bgmPlayer.clip = bgmClips[(int)bgm + randIndex];
        bgmPlayer.Play();
    }

    public void StopBgm()
    {
        bgmPlayer.Stop();
    }
    
    public void PlaySfx(Sfx sfx)
    {
        for(int i=0; i<sfxPlayers.Length; i++)
        {
            int loopIndex = (i + channelIndex) % sfxPlayers.Length;

            if(sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            int ranIndex = 0;
            switch(sfx)
            {
                case Sfx.GotoStage:
                    ranIndex = Random.Range(0, 3);
                    break;
                
                case Sfx.PlayerMove:
                case Sfx.StageClear:
                    ranIndex = Random.Range(0, 2);
                    break;
            }
            
            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    public void SetVolume()
    {
        bgmPlayer.volume = bgmVolume;
        for(int index=0; index<sfxPlayers.Length; index++)
        {
            sfxPlayers[index].volume = sfxVolume;
        }
    }
}
