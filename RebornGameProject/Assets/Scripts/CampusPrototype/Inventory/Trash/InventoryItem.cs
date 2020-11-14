using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private Inventory inventory;
    private Canvas inventoryCanvas;
    private ItemSlot itemSlot;

    private ItemLSH item;
    public ItemLSH Item
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            if (item == null)
            {
                GetComponent<Image>().sprite = null;
            }
            else
            {
                GetComponent<Image>().sprite = item.Sprite;
            }
        }
    }

    private RectTransform rectTransform;
    private Vector2 defaultPosition;
    public int InventoryIndex { get; set; }

    private Image starImage;

    private void Awake()
    {
        inventory = transform.root.GetComponent<Inventory>();
        inventoryCanvas = transform.root.GetComponent<Canvas>();
        itemSlot = transform.parent.GetComponent<ItemSlot>();
        rectTransform = GetComponent<RectTransform>();

        starImage = transform.root.Find("StarImage").GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        defaultPosition = rectTransform.localPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Item != null)
        {
            rectTransform.anchoredPosition += eventData.delta / inventoryCanvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.localPosition = defaultPosition;
    }

    // 드랍을 당하는 곳에서 호출됨
    public void OnDrop(PointerEventData eventData)
    {
        if (Item == null)
        {
            return;
        }

        if (eventData.pointerDrag != null && eventData.pointerDrag != gameObject)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            if (inventoryItem.Item == null)
            {
                return;
            }

            for (int i = 0; i < inventoryItem.Item.CombineItemNames.Length; ++i)
            {
                if (Item.Equals(inventoryItem.Item.CombineItemNames[i]))
                {
                    starImage.rectTransform.position = rectTransform.position;

                    if (InventoryIndex > inventoryItem.InventoryIndex)
                    {
                        Item = null;
                    }
                    else
                    {
                        inventoryItem.Item = null;
                    }

                    //inventory.CombineItem(inventoryItem.item.ResultItems[i], InventoryIndex, inventoryItem.InventoryIndex);

                    break;
                }
            }
        }
    }
}
