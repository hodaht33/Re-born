using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 패널에 마우스 레이캐스팅하여 인벤토리 위 아래 이동 수행
/// </summary>
public class InventoryMouse : MonoBehaviour
{
    // 캔버스 레이캐스팅위한 변수
    private GraphicRaycaster mGraphicRaycaster;
    private PointerEventData mPointerEventData;
    private List<RaycastResult> mRaycastResults;

    private Canvas mCanvas;

    //[SerializeField]
    //private Canvas mInventoryCanvas;
    //private Coroutine mInventoryCoroutine;

    public bool PointerEnabled
    {
        get;
        set;
    }

    private void Awake()
    {
        mCanvas = GetComponent<Canvas>();
        mGraphicRaycaster = mCanvas.GetComponent<GraphicRaycaster>();
        mPointerEventData = new PointerEventData(null);
        mRaycastResults = new List<RaycastResult>();
        //mInventoryCoroutine = Inventory.Instance.CurrentCoroutine;
    }

    private void Update()
    {
        mPointerEventData.position = Input.mousePosition;   // 이벤트 발생 위치를 마우스 위치로 지정
        mRaycastResults.Clear();    // 레이캐스팅 이전 결과 초기화
        mGraphicRaycaster.Raycast(mPointerEventData, mRaycastResults);

        // 마우스가 mCanvas 내의 패널에 레이캐스팅 되면 인벤토리 위로 이동
        // 레이캐스팅 되지 않으면 아래로 이동
        if (mRaycastResults.Count > 0
            && mRaycastResults[0].gameObject.name.Equals("InventoryMousePanel") == true)
        {
            Inventory.Instance.StartAndGetCoroutineUpInventory();
            PointerEnabled = true;
        }
        else
        {
            if (PointerEnabled == true)
            {
                Inventory.Instance.StartAndGetCoroutineDownInventory();
                PointerEnabled = false;
            }
        }
    }
}
