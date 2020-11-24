using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 박서현 아마도ㅎㅎ
/// 기능 : Chapter1 이동
/// </summary>

public class StartClassroom : MonoBehaviour
{
    private Coroutine mTickCoroutine = null;
    [SerializeField]
    private float mActivateTime = 10.0f;
    [SerializeField]
    private Canvas mTexts;

    /*
    public void onClick()
    {
        SceneManager.LoadScene("Chapter1");
    }*/

    private void Start()
    {
        mTickCoroutine = StartCoroutine(TickActivateTime());
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

            //SceneManager.LoadScene("Subway");
            StartCoroutine(PlayCutScene());
            
        }
    }

    private IEnumerator PlayCutScene()
    {
        Coroutine coroutine = FadeManager.Instance.StartCoroutineFadeOut();
        if (coroutine == null)
        {
            yield break;
        }

        yield return coroutine;

        CutSceneManager.Instance.PlayCutScene();

        yield return FadeManager.Instance.StartCoroutineFadeIn();

        enabled = false;
    }

    private IEnumerator TickActivateTime()
    {
        float tickTime = 0.0f;

        while (tickTime <= mActivateTime)
        {
            tickTime = tickTime + Time.deltaTime;

            yield return null;
        }

        ShowText();
    }

    private void ShowText()
    {
        mTexts.enabled = true;
        StartCoroutine(mTexts.transform.Find("Title").gameObject.GetComponent<DissolveShaderStart>().ChangeShaderValue());
        StartCoroutine(mTexts.transform.Find("Text").gameObject.GetComponent<DissolveShaderStart>().ChangeShaderValue());
        StartCoroutine(mTexts.transform.Find("Text2").gameObject.GetComponent<DissolveShaderStart>().ChangeShaderValue());
    }
}
