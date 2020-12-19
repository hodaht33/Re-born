#pragma warning disable CS0649

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatImageClick : MonoBehaviour
{
    [SerializeField]
    private Canvas mCanvas;
    private GraphicRaycaster mGraphicRaycaster;
    private PointerEventData mPointerEventData;
    private List<RaycastResult> mRaycastResults;

    private void Awake()
    {
        mGraphicRaycaster = mCanvas.GetComponent<GraphicRaycaster>();
        mPointerEventData = new PointerEventData(null);
        mRaycastResults = new List<RaycastResult>();
    }

    public bool IsClicked()
    {
        mPointerEventData.position = Input.mousePosition;    // 이벤트 발생 위치를 마우스 위치로 지정
        mRaycastResults.Clear(); // 레이캐스팅 이전 결과 초기화
        mGraphicRaycaster.Raycast(mPointerEventData, mRaycastResults);

        if (mRaycastResults.Count > 0
            && mRaycastResults[0].gameObject.name.Equals(gameObject.name) == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ChangeSprite()
    {

    }
}
