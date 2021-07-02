using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 남자방 입장시 핸드폰 획득
/// </summary>
public class GetPhone : MonoBehaviour
{
    [SerializeField] GameObject phone;  // 핸드폰 패널
    [SerializeField] Message message;
    
    private ItemLSH item;

    private void Start()
    {
        // 아이템 슬롯에 핸드폰 표시 이벤트 추가
        item = GetComponent<ItemLSH>();
        Inventory.Instance.GetItem(item).GetItemEvent += ShowPhone;
        Inventory.Instance.StartAndGetCoroutineUpAndDownInventory();
    }

    // 핸드폰 표시
    public void ShowPhone()
    {
        // 기존 아이템 팝업 숨김
        Inventory.Instance.DeactivateItemPopUp();

        phone.SetActive(true);
        message.ShowMessage();
    }
}