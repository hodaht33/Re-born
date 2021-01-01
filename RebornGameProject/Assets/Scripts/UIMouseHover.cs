using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 마우스 오버 기능(이벤트로 있는 코루틴과 일반함수는 둘 중 하나만 사용)
/// </summary>
public class UIMouseHover : MonoBehaviour
{
    protected delegate void EnableFunc();
    protected event EnableFunc OnEnableFunc;
    protected delegate void DisableFunc();
    protected event DisableFunc OnDisableFunc;

    protected delegate IEnumerator EnableCoroutineFunc();
    protected event EnableCoroutineFunc OnEnableCoroutineFunc;
    protected delegate IEnumerator DisableCoroutineFunc();
    protected event DisableCoroutineFunc OnDisableCoroutineFunc;
    private Coroutine mCoroutine;

    private GraphicRaycaster mGraphicRaycaster;
    private PointerEventData mPointerEventData;
    private List<RaycastResult> mRaycastResults;
    private Canvas mCanvas;

    public bool PointerEnabled
    {
        get;
        set;
    }

    protected void Awake()
    {
        mCanvas = transform.parent.GetComponent<Canvas>();
        mGraphicRaycaster = mCanvas.GetComponent<GraphicRaycaster>();
        mPointerEventData = new PointerEventData(null);
        mRaycastResults = new List<RaycastResult>();
    }

    private void Update()
    {
        mPointerEventData.position = Input.mousePosition;    // 이벤트 발생 위치를 마우스 위치로 지정
        mRaycastResults.Clear(); // 레이캐스팅 이전 결과 초기화
        mGraphicRaycaster.Raycast(mPointerEventData, mRaycastResults);

        if (mRaycastResults.Count > 0
            && mRaycastResults[0].gameObject.Equals(gameObject) == true)
        {
            OnEnableFunc?.Invoke(); // 널이 아니면 호출

            if (mCoroutine != null)
            {
                StopCoroutine(mCoroutine);
            }
            mCoroutine = StartCoroutine(OnEnableCoroutineFunc?.Invoke());

            PointerEnabled = true;
        }
        else
        {
            if (PointerEnabled == true)
            {
                OnDisableFunc?.Invoke();    // 널이 아니면 호출

                if (mCoroutine != null)
                {
                    StopCoroutine(mCoroutine);
                }
                mCoroutine = StartCoroutine(OnDisableCoroutineFunc?.Invoke());

                PointerEnabled = false;
            }
        }
    }

}
