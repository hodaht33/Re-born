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
    private float mHightlightSpeed = 3.0f;
    private Coroutine mCurrentCoroutine;
    private Color mColor;
    private Text mText;


    public void MouseEnter()
    {
        if (mCurrentCoroutine != null)
        {
            StopCoroutine(mCurrentCoroutine);
        }

        mCurrentCoroutine = StartCoroutine(Highlight());
    }

    public void MouseExit()
    {
        if (mCurrentCoroutine != null)
        {
            StopCoroutine(mCurrentCoroutine);
        }

        mCurrentCoroutine = StartCoroutine(ResetHighlight());
    }

    private void Awake()
    {
        //text = GetComponent<Text>();
        mText = GetComponentInChildren<Text>();
    }

    private IEnumerator Highlight()
    {
        mColor = mText.color;
        while (mColor.r < 1.0f)
        {
            yield return null;

            mColor.r += Time.deltaTime * mHightlightSpeed;
            mColor.g += Time.deltaTime * mHightlightSpeed;
            mColor.b += Time.deltaTime * mHightlightSpeed;

            mText.color = mColor;
        }

        mColor.r = 1.0f;
        mColor.g = 1.0f;
        mColor.b = 1.0f;

        mText.color = mColor;
    }

    private IEnumerator ResetHighlight()
    {
        mColor = mText.color;
        while (mColor.r > 0.0f)
        {
            yield return null;

            mColor.r -= Time.deltaTime * mHightlightSpeed;
            mColor.g -= Time.deltaTime * mHightlightSpeed;
            mColor.b -= Time.deltaTime * mHightlightSpeed;

            mText.color = mColor;
        }

        mColor.r = 0.0f;
        mColor.g = 0.0f;
        mColor.b = 0.0f;

        mText.color = mColor;
    }
}
