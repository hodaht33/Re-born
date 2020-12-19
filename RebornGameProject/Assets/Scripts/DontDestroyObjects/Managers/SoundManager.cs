#pragma warning disable CS0649

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetAndPlaySFX("DiaryAlert");
        }
    }

    [SerializeField]
    private AudioClip[] mBgmClips;
    [SerializeField]
    private AudioClip[] mSfxClips;

    private Dictionary<string, AudioClip> mDicBgmAudioClips = new Dictionary<string, AudioClip>();
    private Dictionary<string, AudioClip> mDicSfxAudioClips = new Dictionary<string, AudioClip>();

    private List<AudioChannel> mBgmAudioChannelList;
    private List<AudioChannel> mSfxAudioChannelList;

    private int mBgmChannelCount = 1;
    [SerializeField]
    private int mSfxChannelCount = 3;

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

            for (int i = 0; i < mBgmAudioChannelList.Count; ++i)
            {
                mBgmAudioChannelList[i].MasterVolume = mMasterVolume;
            }

            for (int i = 0; i < mSfxAudioChannelList.Count; ++i)
            {
                mSfxAudioChannelList[i].MasterVolume = mMasterVolume;
            }
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

            for (int i = 0; i < mBgmAudioChannelList.Count; ++i)
            {
                mBgmAudioChannelList[i].MasterVolume = mBgmVolume;
            }
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

            for (int i = 0; i < mSfxAudioChannelList.Count; ++i)
            {
                mSfxAudioChannelList[i].MasterVolume = mSfxVolume;
            }
        }
    }
    #endregion

    // 씬 별 배경음 재생 이벤트 함수
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "MainMenu":
                SetAndPlayBGM("main3-1");
                break;
            case "1-1_Subway":
                //SetAndPlayBGM("");
                break;
            case "1-2_Campus":
                SetAndPlayBGM("Campus");
                break;
            case "1-3_Classroom":
                //SetAndPlayBGM("");
                //SetAndPlayBGM("Classroom");
                break;
            default:
                break;
        }
    }

    // 배경음 설정 및 재생 함수
    public bool SetAndPlayBGM(string clipName)
    {
        if (clipName == "")
        {
            return false;
        }

        if (mDicBgmAudioClips[clipName] == null)
        {
            return false;
        }

        AudioChannel channel;
        for (int i = 0; i < mBgmChannelCount; ++i)
        {
            channel = mBgmAudioChannelList[i];
            if (channel.gameObject.activeInHierarchy == false)
            {
                channel.gameObject.SetActive(true);
                channel.Play(mDicBgmAudioClips[clipName], mMasterVolume);

                break;
            }
        }

        return true;
    }

    // 효과음 설정 및 재생 함수
    public bool SetAndPlaySFX(string clipName)
    {
        if (clipName == "")
        {
            return false;
        }

        if (mDicSfxAudioClips[clipName] == null)
        {
            return false;
        }

        AudioChannel channel;
        for (int i = 0; i < mSfxChannelCount; ++i)
        {
            channel = mSfxAudioChannelList[i];
            if (channel.gameObject.activeInHierarchy == false)
            {
                channel.gameObject.SetActive(true);
                channel.Play(mDicSfxAudioClips[clipName], mMasterVolume);

                break;
            }
        }

        return true;
    }

    /// <summary>
    /// clipName을 All로 하면 모두 중지
    /// </summary>
    public bool StopBgm(string clipName)
    {
        if (clipName.Equals("All") == true)
        {
            for (int i = 0; i < mBgmChannelCount; ++i)
            {
                mBgmAudioChannelList[i].Stop();
            }

            return true;
        }

        for (int i = 0; i < mBgmChannelCount; ++i)
        {
            if (mBgmAudioChannelList[i].StopIfEqualClip(mDicBgmAudioClips[clipName]) == true)
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// clipName을 All로 하면 모두 중지
    /// </summary>
    public bool StopSfx(string clipName)
    {
        if (clipName.Equals("All") == true)
        {
            for (int i = 0; i < mSfxChannelCount; ++i)
            {
                mSfxAudioChannelList[i].Stop();
            }

            return true;
        }

        for (int i = 0; i < mSfxChannelCount; ++i)
        {
            if (mSfxAudioChannelList[i].StopIfEqualClip(mDicSfxAudioClips[clipName]) == true)
            {
                return true;
            }
        }

        return false;
    }

    public void StopAll()
    {
        for (int i = 0; i < mBgmChannelCount; ++i)
        {
            mBgmAudioChannelList[i].Stop();
        }

        for (int i = 0; i < mSfxChannelCount; ++i)
        {
            mSfxAudioChannelList[i].Stop();
        }
    }

    //// 배경음 재생 함수
    //public void PlayBGM()
    //{
    //    mBgmAudioSource.Play();
    //}

    //// 효과음 재생 함수
    //public void PlaySFX()
    //{
    //    mSfxAudioSource.Play();
    //}

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
            mDicBgmAudioClips.Add(clip.name, clip);
        }

        // 효과음 클립 저장
        foreach (AudioClip clip in mSfxClips)
        {
            mDicSfxAudioClips.Add(clip.name, clip);
        }

        mBgmAudioChannelList = new List<AudioChannel>();
        mSfxAudioChannelList = new List<AudioChannel>();

        // 배경음 플레이어 생성 및 기본설정
        for (int i = 1; i <= mBgmChannelCount; ++i)
        {
            GameObject bgmPlayer = new GameObject("BGMChannel" + i);
            bgmPlayer.transform.SetParent(transform);

            mBgmAudioChannelList.Add(bgmPlayer.AddComponent<AudioChannel>());
        }

        // 효과음 플레이어 생성 및 기본설정
        for (int i = 1; i <= mSfxChannelCount; ++i)
        {
            GameObject sfxPlayer = new GameObject("SFXChannel" + i);
            sfxPlayer.transform.SetParent(transform);

            mSfxAudioChannelList.Add(sfxPlayer.AddComponent<AudioChannel>());
        }
    }

    private void Start()
    {
        // 씬 로드 시 실행될 이벤트 함수 추가
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 메인화면 배경음 설정 및 재생
        SetAndPlayBGM("main3-1");
    }
}