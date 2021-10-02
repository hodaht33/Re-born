using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 텍스트 하이라이트
/// </summary>
public class CustomDropdownText : MonoBehaviour
{
    [SerializeField]
    private float mHightlightSpeed = 3.0f;  // 하이라이트 색상 변경 속도
    private Coroutine mCurrentCoroutine;
    private Color mColor;
    private Text mText;

    // 프리팹으로 만들어둔 ResolutionDropdownItem에
    // MouseEnter와 MouseExit이벤트 등록되어 있음
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

    // 글씨 색을 흰색으로 하이라이트 처리하는 코루틴
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

    // 글씨 색을 검은색으로 되돌리는 코루틴
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
