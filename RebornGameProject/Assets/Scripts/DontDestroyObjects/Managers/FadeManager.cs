#pragma warning disable CS0414  // 문제없는 warning해제

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : Fade 효과 관리
/// </summary>
public class FadeManager : SingletonBase<FadeManager>
{
    private Canvas mCanvas;
    private Image mFadeImage;
    private bool mbPlay;
    private Coroutine mCoroutine;

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeInCoroutine());
    }

    public Coroutine StartAndGetCoroutineFadeInOrNull()
    {
        if (mCoroutine != null)
        {
            StopCoroutine(mCoroutine);
        }
        //if (mbPlay == true)
        //{
        //    return null;
        //}

        return mCoroutine = StartCoroutine(FadeInCoroutine());
    }

    public Coroutine StartAndGetCoroutineFadeOutOrNull()
    {
        if (mCoroutine != null)
        {
            StopCoroutine(mCoroutine);
        }
        //if (mbPlay == true)
        //{
        //    return null;
        //}

        return mCoroutine = StartCoroutine(FadeOutCoroutine());
    }

    private void Awake()
    {
        if (instance != null
            && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        mFadeImage = transform.GetChild(0).GetComponent<Image>();
        mCanvas = GetComponent<Canvas>();
        mCanvas.enabled = true;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 0->1
    private IEnumerator FadeInCoroutine()
    {
        mbPlay = true;

        Color color = mFadeImage.color;

        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime * 0.8f;

            mFadeImage.color = color;

            yield return null;
        }

        color.a = 0.0f;
        mFadeImage.color = color;

        mbPlay = false;
        mCanvas.enabled = false;
    }

    // 1->0
    private IEnumerator FadeOutCoroutine()
    {
        mbPlay = true;
        mCanvas.enabled = true;

        Color color = mFadeImage.color;

        while (color.a < 1.0f)
        {
            color.a += Time.deltaTime * 0.8f;

            mFadeImage.color = color;

            yield return null;
        }

        color.a = 1.0f;
        mFadeImage.color = color;

        mbPlay = false;
    }
}
