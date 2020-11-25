using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 박서현
/// 기능 : Chapter1 이동
/// </summary>
public class StartClassroom : MonoBehaviour
{
    private Coroutine mTickCoroutine = null;
    [SerializeField]
    private float mActivateTime = 10.0f;
    [SerializeField]
    private Canvas mTexts;
    [SerializeField]
    private DissolveShaderStart mTitle;
    [SerializeField]
    private DissolveShaderStart mText1;
    [SerializeField]
    private DissolveShaderStart mText2;

    private void Awake()
    {
        enabled = false;
        mTickCoroutine = StartCoroutine(TickActivateTimeCoroutine());
        mTexts.enabled = false;
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Escape) == true
            || Input.GetMouseButtonDown(0) == true
            || Input.GetMouseButtonDown(1) == true
            || Input.GetMouseButtonDown(2) == true)
            {
                return;
            }
            
            StartCoroutine(PlayCutSceneCoroutine());
            enabled = false;
        }
    }

    private IEnumerator PlayCutSceneCoroutine()
    {
        Coroutine coroutine = FadeManager.Instance.StartAndGetCoroutineFadeOutOrNull();

        yield return coroutine;

        CutSceneManager.Instance.PlayCutScene();

        yield return FadeManager.Instance.StartAndGetCoroutineFadeInOrNull();

        enabled = false;
    }

    private IEnumerator TickActivateTimeCoroutine()
    {
        float tickTime = 0.0f;

        while (tickTime <= mActivateTime)
        {
            tickTime = tickTime + Time.deltaTime;

            yield return null;
        }

        StartCoroutine(ShowText());
    }

    //private void ShowText()
    //{
    //    mTexts.enabled = true;
    //    StartCoroutine(mTitle.ChangeShaderValueCoroutine());
    //    StartCoroutine(mText1.ChangeShaderValueCoroutine());
    //    StartCoroutine(mText2.ChangeShaderValueCoroutine());

    //    enabled = true;
    //}

    private IEnumerator ShowText()
    {
        mTexts.enabled = true;
        StartCoroutine(mTitle.ChangeShaderValueCoroutine());
        StartCoroutine(mText1.ChangeShaderValueCoroutine());
        yield return StartCoroutine(mText2.ChangeShaderValueCoroutine());

        enabled = true;
    }
}
