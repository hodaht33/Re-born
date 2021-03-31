using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 마우스 오버 시 페이드 적용
/// 
/// **현재 사용되지 않음**
/// 
/// </summary>
public class FadeByHover : UIMouseHover
{
    [SerializeField]
    private Image targetImage;
    [SerializeField]
    private float fadeSpeed = 2.0f;

    private new void Awake()
    {
        base.Awake();
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }
        OnEnableCoroutineFunc += FadeIn;
        OnDisableCoroutineFunc += FadeOut;
    }

    private IEnumerator FadeIn()
    {
        Color c = targetImage.color;
        while (c.a < 1.0f )
        {
            c.a += fadeSpeed * Time.deltaTime;
            targetImage.color = c;

            yield return null;
        }

        c.a = 1.0f;
        targetImage.color = c;
    }

    private IEnumerator FadeOut()
    {
        Color c = targetImage.color;
        while (c.a > 0.0f)
        {
            c.a -= fadeSpeed * Time.deltaTime;
            targetImage.color = c;

            yield return null;
        }

        c.a = 0.0f;
        targetImage.color = c;
    }
}
