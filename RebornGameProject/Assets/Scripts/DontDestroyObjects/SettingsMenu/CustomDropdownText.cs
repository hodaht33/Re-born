using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 텍스트 하이라이트
/// </summary>
public class CustomDropdownText : MonoBehaviour
{
    [SerializeField]
    private float hightlightSpeed = 3.0f;
    private Coroutine currentCoroutine;
    private Color color;
    private Text text;

    private void Awake()
    {
        //text = GetComponent<Text>();
        text = GetComponentInChildren<Text>();
    }

    public void MouseEnter()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(Highlight());
    }

    public void MouseExit()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(ResetHighlight());
    }

    private IEnumerator Highlight()
    {
        color = text.color;
        while (color.r < 1.0f)
        {
            yield return null;

            color.r += Time.deltaTime * hightlightSpeed;
            color.g += Time.deltaTime * hightlightSpeed;
            color.b += Time.deltaTime * hightlightSpeed;

            text.color = color;
        }

        color.r = 1.0f;
        color.g = 1.0f;
        color.b = 1.0f;

        text.color = color;
    }

    private IEnumerator ResetHighlight()
    {
        color = text.color;
        while (color.r > 0.0f)
        {
            yield return null;

            color.r -= Time.deltaTime * hightlightSpeed;
            color.g -= Time.deltaTime * hightlightSpeed;
            color.b -= Time.deltaTime * hightlightSpeed;

            text.color = color;
        }

        color.r = 0.0f;
        color.g = 0.0f;
        color.b = 0.0f;

        text.color = color;
    }
}
