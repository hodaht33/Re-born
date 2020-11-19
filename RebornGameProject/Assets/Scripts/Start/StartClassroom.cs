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

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("Chapter1");
        }
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
        StartCoroutine(texts.transform.Find("Title").gameObject.GetComponent<DissolveShaderStart>().ChangeShaderValue());
        StartCoroutine(texts.transform.Find("Text").gameObject.GetComponent<DissolveShaderStart>().ChangeShaderValue());
        StartCoroutine(texts.transform.Find("Text2").gameObject.GetComponent<DissolveShaderStart>().ChangeShaderValue());
    }
}
