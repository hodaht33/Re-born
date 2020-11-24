using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeManager : SingletonBase<FadeManager>
{
    private Canvas canvas;
    private Image fadeImage;
    private bool isPlay;

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

        fadeImage = transform.GetChild(0).GetComponent<Image>();
        canvas = GetComponent<Canvas>();
        canvas.enabled = true;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeIn());
    }

    private Coroutine coroutine = null;

    public Coroutine StartCoroutineFadeIn()
    {
        if (isPlay == true)
        {
            return null;
        }

        return StartCoroutine(FadeIn());
    }

    public Coroutine StartCoroutineFadeOut()
    {
        if (isPlay == true)
        {
            return null;
        }

        return StartCoroutine(FadeOut());
    }

    // 0->1
    private IEnumerator FadeIn()
    {
        isPlay = true;

        Color color = fadeImage.color;

        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime * 0.8f;

            fadeImage.color = color;

            yield return null;
        }

        color.a = 0.0f;
        fadeImage.color = color;

        isPlay = false;
        canvas.enabled = false;
    }

    // 1->0
    private IEnumerator FadeOut()
    {
        isPlay = true;
        canvas.enabled = true;

        Color color = fadeImage.color;

        while (color.a < 1.0f)
        {
            color.a += Time.deltaTime * 0.8f;

            fadeImage.color = color;

            yield return null;
        }

        color.a = 1.0f;
        fadeImage.color = color;

        isPlay = false;
    }
}
