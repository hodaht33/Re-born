using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DissolveImage : MonoBehaviour
{
    private Material mat;

    [SerializeField]
    private float speed = 0.5f;

    private Coroutine dissolveCoroutine;

    private void Awake()
    {
        mat = GetComponent<Image>().material;
    }

    public Coroutine StartDissolve()
    {
        return dissolveCoroutine = StartCoroutine(ChangeShaderValue());
    }

    private IEnumerator ChangeShaderValue()
    {
        float deltaVal = 0.0f;

        while (deltaVal < 0.1f)
        {
            deltaVal += Time.deltaTime * speed;
            mat.SetFloat("_Edges", deltaVal);

            yield return null;
        }

        deltaVal = 0.0f;

        while (deltaVal <= 1.0f)
        {
            deltaVal += Time.deltaTime * speed;
            mat.SetFloat("_Level", deltaVal);

            yield return null;
        }
    }

    public void SetDefault()
    {
        if (dissolveCoroutine != null)
        {
            StopCoroutine(dissolveCoroutine);
        }

        mat.SetFloat("_Level", 0.0f);
        mat.SetFloat("_Edges", 0.0f);
    }
}
