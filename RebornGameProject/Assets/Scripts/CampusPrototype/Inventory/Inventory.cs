using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<ItemSlot> items;   // 아이템 슬롯 관리 리스트
    private RectTransform itemPanel;

    private void Awake()
    {
        items = new List<ItemSlot>(8);
        itemPanel = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();    // 0번째를 원본으로 복사본 생성
        items.Add(itemPanel.GetComponent<ItemSlot>());

        // 슬롯 추가
        for (int i = 1; i < items.Capacity; ++i)
        {
            RectTransform panel = Instantiate(itemPanel, transform.GetChild(0));
            panel.anchoredPosition = new Vector2(panel.anchoredPosition.x + 50 * i, panel.anchoredPosition.y);
            items.Add(panel.GetComponent<ItemSlot>());
        }

        transform.Find("DragImage").SetParent(transform.Find("Inventory"));
    }

    public bool GetItem(ItemLSH item)
    {
        // 아이템 획득
        for (int i = 0; i < 8; ++i)
        {
            if (items[i].Item == null)
            {
                items[i].Item = item;
                break;
            }
        }

        return true;
    }

    // 아이템 사용
    public bool UseItem(string itemName)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            if (items[i].Item != null && itemName.Equals(items[i].Item.ItemName.Substring(0, itemName.Length)))
            {
                items[i].Item = null;

                return true;
            }
        }

        return false;
    }

    // 아이템 찾기
    public bool FindItem(string itemName)
    {
        for (int i = 0; i < items.Count; ++i)
        {
            if (itemName.Equals(items[i].Item.ItemName))
            {
                return true;
            }
        }

        return false;
    }
}
