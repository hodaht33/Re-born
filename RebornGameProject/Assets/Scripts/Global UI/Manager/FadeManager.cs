#pragma warning disable CS0414  // 문제없는 warning해제

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : Fade 효과 관리
/// </summary>
public class FadeManager : SingletonBase<FadeManager>
{
    private Canvas mCanvas; // 페이드 인-아웃하는 캔버스
    private Image mFadeImage;   // 페이드 효과를 위해 알파값을 조절하는 이미지
    private bool mbPlay;    // 페이드 재생 여부
    private Coroutine mCoroutine;

    // 씬 전환 이벤트 메서드
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FadeInCoroutine());
    }

    // 페이드 인 코루틴 호출 메서드
    public Coroutine StartAndGetCoroutineFadeIn()
    {
        if (mCoroutine != null)
        {
            StopCoroutine(mCoroutine);
        }

        return mCoroutine = StartCoroutine(FadeInCoroutine());
    }

    // 페이드 아웃 코루틴 호출 메서드
    public Coroutine StartAndGetCoroutineFadeOut()
    {
        if (mCoroutine != null)
        {
            StopCoroutine(mCoroutine);
        }

        return mCoroutine = StartCoroutine(FadeOutCoroutine());
    }

    protected override void Awake()
    {
        base.Awake();

        mFadeImage = transform.GetChild(0).GetComponent<Image>();
        mCanvas = GetComponent<Canvas>();
        mCanvas.enabled = true;

        // 씬 전환 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 페이드 인 실행 코루틴
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

    // 페이드 아웃 실행 코루틴
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
