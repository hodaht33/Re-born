using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 손잡이 위 아래 이동(임시방편), 이동 시 퍼즐 답 제출
/// </summary>
public class Handle : MonoBehaviour
{
    public delegate void CheckAnswer(Handle handle);
    public event CheckAnswer OnCheckAnswer;

    [SerializeField]
    private float mMinusPosY = 2.0f;    // 내려가는 정도
    private Vector3 mDefaultPos;    // 원본 위치
    [SerializeField, Range(1.0f, 100.0f)]
    private float mMoveSpeed = 10.0f;
    private Coroutine mCoroutine;
    private bool mbPull;
    public bool IsPull
    {
        get
        {
            return mbPull;
        }
        set
        {
            // 값이 다를 때만 적용
            if (mbPull != value)
            {
                mbPull = value;

                if (mbPull == true)
                {
                    if (mCoroutine != null)
                    {
                        StopCoroutine(mCoroutine);
                    }
                    mCoroutine = StartCoroutine(Pull());
                }
                else
                {
                    if (mCoroutine != null)
                    {
                        StopCoroutine(mCoroutine);
                    }
                    mCoroutine = StartCoroutine(BackToDefaultPos());
                }
            }
        }
    }

    private void Awake()
    {
        // 원본 위치 저장
        mDefaultPos = transform.position;
    }

    // 마우스 클릭 이벤트
    private void OnMouseUp()
    {
        if (IsPull == false)
        {
            IsPull = true;
            OnCheckAnswer(this);
        }
    }

    // 밑으로 내리는 코루틴
    private IEnumerator Pull()
    {
        Vector3 dest = new Vector3(mDefaultPos.x, mDefaultPos.y - mMinusPosY, mDefaultPos.z);
        while (transform.position.y > dest.y)

        {
            transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime * mMoveSpeed);

            yield return null;
        }
    }

    // 다시 원위치시키는 코루틴
    private IEnumerator BackToDefaultPos()
    {
        while (transform.position.y < mDefaultPos.y)
        {
            transform.position = Vector3.Lerp(transform.position, mDefaultPos, Time.deltaTime * mMoveSpeed);

            yield return null;
        }
    }
}
