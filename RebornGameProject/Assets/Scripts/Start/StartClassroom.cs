using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartClassroom : MonoBehaviour
{
    private Coroutine tickCoroutine = null;
    [SerializeField]
    private float activateTime = 10.0f;
    [SerializeField]
    private Canvas texts;

    /*
    public void onClick()
    {
        SceneManager.LoadScene("Chapter1");
    }*/

    private void Start()
    {
        tickCoroutine = StartCoroutine(TickActivateTime());
        texts.enabled = false;
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
        Coroutine coroutine = FadeManager.Instance.StartAndGetCoroutineFadeOutOrNull();
        if (coroutine == null)
        {
            yield break;
        }

        yield return coroutine;

        CutSceneManager.Instance.PlayCutScene();

        yield return FadeManager.Instance.StartAndGetCoroutineFadeInOrNull();

        enabled = false;
    }

    private IEnumerator TickActivateTime()
    {
        float tickTime = 0.0f;

        while (tickTime <= activateTime)
        {
            tickTime = tickTime + Time.deltaTime;

            yield return null;
        }

        ShowText();
    }

    private void ShowText()
    {
        texts.enabled = true;
        StartCoroutine(texts.transform.Find("Title").gameObject.GetComponent<DissolveShaderStart>().ChangeShaderValueCoroutine());
        StartCoroutine(texts.transform.Find("Text").gameObject.GetComponent<DissolveShaderStart>().ChangeShaderValueCoroutine());
        StartCoroutine(texts.transform.Find("Text2").gameObject.GetComponent<DissolveShaderStart>().ChangeShaderValueCoroutine());
    }
}
