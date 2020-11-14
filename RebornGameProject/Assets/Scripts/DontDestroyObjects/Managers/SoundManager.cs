using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 사운드 관리
/// </summary>
public class SoundManager : SingletonBase<SoundManager>
{
    #region 멤버변수
    [SerializeField]
    private AudioClip[] bgmClips;
    [SerializeField]
    private AudioClip[] sfxClips;

    private Dictionary<string, AudioClip> dicBGMAudioClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> dicSFXAudioClips = new Dictionary<string, AudioClip>();

    private AudioSource bgmAudioSource;
    private AudioSource sfxAudioSource;

    #region 볼륨
    // 기획서 상 소리 설정은 하나이므로 MasterVolume사용
    [SerializeField]
    private float masterVolume = 0.5f;
    public float MasterVolume
    {
        get { return masterVolume; }
        set
        {
            masterVolume = value;
            bgmAudioSource.volume = masterVolume;
            sfxAudioSource.volume = masterVolume;
        }
    }

    //[SerializeField]
    private float bgmVolume = 0.5f;
    public float BGMVolume
    {
        get { return bgmVolume; }
        set
        {
            bgmVolume = value;
            bgmAudioSource.volume = bgmVolume;
        }
    }

    //[SerializeField]
    private float sfxVolume = 0.5f;
    public float SFXVolume
    {
        get { return sfxVolume; }
        set
        {
            sfxVolume = value;
            sfxAudioSource.volume = SFXVolume;
        }
    }
    #endregion
    #endregion

    #region 초기화 Awake함수
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        // 배경음 클립 저장
        foreach(AudioClip clip in bgmClips)
        {
            dicBGMAudioClips.Add(clip.name, clip);
        }

        // 효과음 클립 저장
        foreach (AudioClip clip in sfxClips)
        {
            dicSFXAudioClips.Add(clip.name, clip);
        }

        // 배경음 플레이어 생성 및 기본설정
        GameObject bgmPlayer = new GameObject("BGMPlayer");
        bgmPlayer.transform.SetParent(transform);
        bgmAudioSource = bgmPlayer.AddComponent<AudioSource>();
        bgmAudioSource.playOnAwake = false;
        bgmAudioSource.loop = true;
        bgmAudioSource.volume = masterVolume;

        // 효과음 플레이어 생성 및 기본설정
        GameObject sfxPlayer = new GameObject("SFXPlayer");
        sfxPlayer.transform.SetParent(transform);
        sfxAudioSource = sfxPlayer.AddComponent<AudioSource>();
        sfxAudioSource.playOnAwake = false;
        sfxAudioSource.loop = false;
        sfxAudioSource.volume = masterVolume;
    }
    #endregion

    private void Start()
    {
        // 씬 로드 시 실행될 이벤트 함수 추가
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 메인화면 배경음 설정 및 재생
        SetAndPlayBGM("main3-1");
    }

    #region 씬 별 배경음 재생 이벤트 함수
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Start":
                SetAndPlayBGM("main3-1");
                break;
            case "Subway":
                break;
            case "classroom":
                SetAndPlayBGM("campus1");
                break;
            default:
                break;
        }
    }
    #endregion

    #region 배경음 설정 및 재생 함수
    public bool SetAndPlayBGM(string clipName)
    {
        if (dicBGMAudioClips[clipName] == null)
        {
            return false;
        }

        bgmAudioSource.clip = dicBGMAudioClips[clipName];
        bgmAudioSource.Play();

        return true;
    }
    #endregion

    #region 효과음 설정 및 재생 함수
    public bool SetAndPlaySFX(string clipName)
    {
        if (dicSFXAudioClips[clipName] == null)
        {
            return false;
        }

        sfxAudioSource.clip = dicSFXAudioClips[clipName];
        sfxAudioSource.Play();

        return true;
    }
    #endregion

    // 배경음 재생 함수
    public void PlayBGM()
    {
        bgmAudioSource.Play();
    }

    // 효과음 재생 함수
    public void PlaySFX()
    {
        sfxAudioSource.Play();
    }
}
