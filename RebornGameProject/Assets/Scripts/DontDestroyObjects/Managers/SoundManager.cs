#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 오디오 채널 관리
/// </summary>
public class SoundManager : SingletonBase<SoundManager>
{
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
        switch (SceneInfo.GetSceneEnum(scene.name))
        {
            case SceneInfo.EScene.MainMenu:
                SetAndPlayBGM(SoundInfo.EBgmList.Main);
                break;
            case SceneInfo.EScene.Subway:
                SetAndPlayBGM(SoundInfo.EBgmList.Subway);
                break;
            case SceneInfo.EScene.Campus:
                SetAndPlayBGM(SoundInfo.EBgmList.Campus);
                break;
            case SceneInfo.EScene.Classroom:
                SetAndPlayBGM(SoundInfo.EBgmList.Calssroom);
                break;
            case SceneInfo.EScene.boysRoom:
                break;
            case SceneInfo.EScene.girlsRoom:
                break;
            case SceneInfo.EScene.End:
                break;
            case SceneInfo.EScene.NULL:
                break;
        }
    }

    /// <summary>
    /// 배경음 설정 및 재생
    /// </summary>
    public void SetAndPlayBGM(SoundInfo.EBgmList bgm)
    {
        AudioChannel channel = mBgmAudioChannelList[0];
        channel.Play(SoundInfo.GetBgmClip(bgm), mMasterVolume);
    }

    /// <summary>
    /// 효과음 설정 및 재생
    /// </summary>
    public void SetAndPlaySFX(SoundInfo.ESfxList sfx)
    {
        AudioChannel channel;
        for (int i = 0; i < mSfxChannelCount; ++i)
        {
            channel = mSfxAudioChannelList[i];
            if (channel.gameObject.activeInHierarchy == false)
            {
                channel.gameObject.SetActive(true);
                channel.Play(SoundInfo.GetSfxClip(sfx), mMasterVolume);

                break;
            }
        }
    }

    /// <summary>
    /// 특정 배경음 재생 중지
    /// </summary>
    public void StopEqualBgm(SoundInfo.EBgmList bgm)//, bool isStopAll)
    {
        if (mBgmAudioChannelList[0].AudioClipOrNull.Equals(SoundInfo.GetBgmClip(bgm)) == true)
        {
            mBgmAudioChannelList[0].Stop();

            return;
        }

        for (int i = 0; i < mBgmChannelCount; ++i)
        {
            if (mBgmAudioChannelList[i].StopIfEqualClip(SoundInfo.GetBgmClip(bgm)) == true)
            {
                return;
            }
        }
    }

    /// <summary>
    /// 특정 효과음 재생 중지, isStopAll을 true로 하면 해당 효과음을 모두 중지
    /// </summary>
    public void StopSfx(SoundInfo.ESfxList sfx, bool isStopAll)
    {
        if (isStopAll == true)
        {
            foreach (AudioChannel channel in mSfxAudioChannelList)
            {
                if (channel.AudioClipOrNull.Equals(SoundInfo.GetSfxClip(sfx)) == true)
                {
                    channel.Stop();
                }
            }
        }
        else
        {
            foreach (AudioChannel channel in mSfxAudioChannelList)
            {
                if (channel.AudioClipOrNull.Equals(SoundInfo.GetSfxClip(sfx)) == true)
                {
                    channel.Stop();

                    return;
                }
            }
        }
    }
    
    public void StopAllSound()
    {
        foreach(AudioChannel channel in mBgmAudioChannelList)
        {
            channel.Stop();
        }

        foreach (AudioChannel channel in mSfxAudioChannelList)
        {
            channel.Stop();
        }
    }

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
        SetAndPlayBGM(SoundInfo.EBgmList.Main);
    }
}