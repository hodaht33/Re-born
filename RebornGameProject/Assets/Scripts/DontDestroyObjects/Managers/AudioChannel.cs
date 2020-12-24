using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 오디오 채널 기능 수행
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioChannel : MonoBehaviour
{
    private AudioSource audioSource;
    private string mClipName;
    public string ClipName
    {
        get
        {
            return mClipName;
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
        mClipName = clip.name;
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    public void Stop()
    {
        audioSource.clip = null;
        audioSource.Stop();
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
        audioSource.playOnAwake = false;
        audioSource.volume = mMasterVolume;

        if (gameObject.name.Substring(0, 3).Equals("BGM") == true)
        {
            audioSource.loop = true;
        }
        else
        {
            audioSource.loop = false;
        }

        gameObject.SetActive(false);
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
