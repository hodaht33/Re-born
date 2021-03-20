using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 오디오 재생 및 중지
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioChannel : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip AudioClipOrNull
    {
        get
        {
            return audioSource.clip;
        }
    }

    private float mMasterVolume;
    public float MasterVolume
    {
        set
        {
            mMasterVolume = value;
            audioSource.volume = mMasterVolume;
        }
    }

    // 오디오 재생 메서드
    public void Play(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    // 오디오 중지 메서드
    public void Stop()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

    // 인자로 들어온 클립과 같은 클립이 재생 중이면 중지시키는 메서드
    public bool StopIfEqualClip(AudioClip clip)
    {
        if (audioSource.isPlaying == true
            && audioSource.clip.Equals(clip) == true)
        {
            audioSource.Stop();

            return true;
        }

        return false;
    }
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = mMasterVolume;

        // 배경음과 효과음에 따른 초기 설정
        if (gameObject.name.Substring(0, 3).Equals("BGM") == true)
        {
            audioSource.playOnAwake = true;
            audioSource.loop = true;
        }
        else
        {
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // 오디오 재생 중지 시 자동으로 클립 삭제 및 비활성화
        if (audioSource.isPlaying == false)
        {
            audioSource.clip = null;
            gameObject.SetActive(false);
        }
    }
}
