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
    private AudioClip[] mBgmClips;
    [SerializeField]
    private AudioClip[] mSfxClips;

    private Dictionary<string, AudioClip> mDicBGMAudioClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> mDicSFXAudioClips = new Dictionary<string, AudioClip>();

    private AudioSource mBgmAudioSource;
    private AudioSource mSfxAudioSource;

    #region 볼륨
    // 기획서 상 소리 설정은 하나이므로 MasterVolume사용
    [SerializeField]
    private float mMasterVolume = 0.5f;
    public float MasterVolume
    {
        get
        {
            return mMasterVolume;
        }
        set
        {
            mMasterVolume = value;
            mBgmAudioSource.volume = mMasterVolume;
            mSfxAudioSource.volume = mMasterVolume;
        }
    }

    //[SerializeField]
    private float mBgmVolume = 0.5f;
    public float BGMVolume
    {
        get
        {
            return mBgmVolume;
        }
        set
        {
            mBgmVolume = value;
            mBgmAudioSource.volume = mBgmVolume;
        }
    }

    //[SerializeField]
    private float mSfxVolume = 0.5f;
    public float SFXVolume
    {
        get
        {
            return mSfxVolume;
        }
        set
        {
            mSfxVolume = value;
            mSfxAudioSource.volume = SFXVolume;
        }
    }
    #endregion
    #endregion

    #region 씬 별 배경음 재생 이벤트 함수
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "Start":
                SetAndPlayBGM("main3-1");
                break;
            case "Subway":
                //SetAndPlayBGM("");
                break;
            case "Campus":
                SetAndPlayBGM("Campus");
                break;
            case "Classroom":
                //SetAndPlayBGM("");
                //SetAndPlayBGM("Classroom");
                break;
            default:
                break;
        }
    }
    #endregion

    #region 배경음 설정 및 재생 함수
    public bool SetAndPlayBGM(string clipName)
    {
        if (clipName == "")
        {
            mBgmAudioSource.Stop();

            return true;
        }

        if (mDicBGMAudioClips[clipName] == null)
        {
            return false;
        }

        mBgmAudioSource.clip = mDicBGMAudioClips[clipName];
        mBgmAudioSource.Play();

        return true;
    }
    #endregion

    #region 효과음 설정 및 재생 함수
    public bool SetAndPlaySFX(string clipName)
    {
        if (clipName == "")
        {
            mBgmAudioSource.Stop();

            return true;
        }

        if (mDicSFXAudioClips[clipName] == null)
        {
            return false;
        }

        mSfxAudioSource.clip = mDicSFXAudioClips[clipName];
        mSfxAudioSource.Play();

        return true;
    }
    #endregion

    // 배경음 재생 함수
    public void PlayBGM()
    {
        mBgmAudioSource.Play();
    }

    // 효과음 재생 함수
    public void PlaySFX()
    {
        mSfxAudioSource.Play();
    }

    #region 초기화 Awake함수
    private void Awake()
    {
        if (instance != null &&
            instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        // 배경음 클립 저장
        foreach (AudioClip clip in mBgmClips)
        {
            mDicBGMAudioClips.Add(clip.name, clip);
        }

        // 효과음 클립 저장
        foreach (AudioClip clip in mSfxClips)
        {
            mDicSFXAudioClips.Add(clip.name, clip);
        }

        // 배경음 플레이어 생성 및 기본설정
        GameObject bgmPlayer = new GameObject("BGMPlayer");
        bgmPlayer.transform.SetParent(transform);
        mBgmAudioSource = bgmPlayer.AddComponent<AudioSource>();
        mBgmAudioSource.playOnAwake = false;
        mBgmAudioSource.loop = true;
        mBgmAudioSource.volume = mMasterVolume;

        // 효과음 플레이어 생성 및 기본설정
        GameObject sfxPlayer = new GameObject("SFXPlayer");
        sfxPlayer.transform.SetParent(transform);
        mSfxAudioSource = sfxPlayer.AddComponent<AudioSource>();
        mSfxAudioSource.playOnAwake = false;
        mSfxAudioSource.loop = false;
        mSfxAudioSource.volume = mMasterVolume;
    }
    #endregion

    private void Start()
    {
        // 씬 로드 시 실행될 이벤트 함수 추가
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 메인화면 배경음 설정 및 재생
        SetAndPlayBGM("main3-1");
    }
}
