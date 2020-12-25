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

    public void Play(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.Stop();
        audioSource.clip = null;
    }

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
        if (audioSource.isPlaying == false)
        {
            audioSource.clip = null;
            gameObject.SetActive(false);
        }
    }
}
