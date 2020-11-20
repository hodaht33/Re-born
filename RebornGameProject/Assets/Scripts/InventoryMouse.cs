using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryMouse : MonoBehaviour
{
    private GraphicRaycaster graphicRaycaster;
    private PointerEventData pointerEventData;
    private List<RaycastResult> raycastResults;
    private Canvas canvas;

    [SerializeField] private Canvas inventoryCanvas;
    private Coroutine inventoryCoroutine;
    
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        pointerEventData = new PointerEventData(null);
        raycastResults = new List<RaycastResult>();
        inventoryCoroutine = Inventory.Instance.CurrentCoroutine;
    }

    private void Update()
    {
        pointerEventData.position = Input.mousePosition;    // 이벤트 발생 위치를 마우스 위치로 지정
        raycastResults.Clear(); // 레이캐스팅 이전 결과 초기화
        graphicRaycaster.Raycast(pointerEventData, raycastResults);
        
        if (raycastResults.Count == 0)
        {
            if (inventoryCoroutine != null)
            {
                StopCoroutine(inventoryCoroutine);
            }
            inventoryCoroutine = StartCoroutine(Inventory.Instance.DownInventory());

            return;
        }

        if (raycastResults[0].gameObject.name.Equals("InventoryMousePanel"))
        {
            if (inventoryCoroutine != null)
            {
                StopCoroutine(inventoryCoroutine);
            }
            inventoryCoroutine = StartCoroutine(Inventory.Instance.UpInventory());
        }
    }
    
    
}
