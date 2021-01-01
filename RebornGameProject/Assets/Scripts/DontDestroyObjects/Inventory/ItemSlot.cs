using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 인벤토리 내 슬롯 관리
/// </summary>
public class ItemSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private RectTransform mDragImageRT;  // 드래그 시 보여줄 이미지의 RectTransform
    private Sprite mDefaultImage;        // 기본 UIMask이미지
    private bool mbBeginDragNull;         // 드래그 시작 부분이 널인지 체크를 위한 멤버
    private ParticleSystem mParticle;    // 파티클 시스템
    public ParticleSystem Particle
    {
        get
        {
            return mParticle;
        }
    }

    private Image mSlotImage;

    private Color mOriginalColor;

    private bool mbSelected;
    public bool IsSelected
    {
        get { return mbSelected; }
        set
        {
            mbSelected = value;
            if (mbSelected == true)
            {
                mSlotImage.color = Color.red;
            }
            else
            {
                mSlotImage.color = mOriginalColor;
            }
        }
    }

    private ItemLSH mItem;   // 현재 슬롯이 가지는 아이템
    public ItemLSH Item     // 현재 슬롯의 아이템이 변경되면 이미지 변경
    {
        get { return mItem; }
        set
        {
            mItem = value;
            if (mItem == null)
            {
                transform.GetChild(0).GetComponent<Image>().sprite = mDefaultImage;
            }
            else
            {
                transform.GetChild(0).GetComponent<Image>().sprite = mItem.Sprite;
            }
        }
    }

    private RectTransform mRectTransform;        // 이 오브젝트의 RectTransform
    private RectTransform mItemRectTransform;    // 자식인 아이템의 RectTransform
    private Canvas mInventoryCanvas;             // 인벤토리 캔버스

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 아이템이 없으면 종료
        if (Item == null)
        {
            mbBeginDragNull = true;

            return;
        }

        // 드래그 이미지 초기화
        mDragImageRT.gameObject.SetActive(true);
        mDragImageRT.GetComponent<Image>().sprite = Item.Sprite;
        mDragImageRT.anchoredPosition = mRectTransform.anchoredPosition;

        // 아이템의 이미지 컴포넌트 잠시 끔
        transform.GetChild(0).GetComponent<Image>().enabled = false;
        GetComponent<Image>().raycastTarget = false;    // 자기 자신이 드랍되지 않게 하기위햏 레이캐스트타겟 끔

        // 드래그하는 아이템은 선택되지 않도록 변경
        IsSelected = false;
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        // 아이템이 없으면 종료
        if (Item == null)
        {
            return;
        }

        // 드래그 이미지 위치 변경
        mDragImageRT.anchoredPosition += eventData.delta / mInventoryCanvas.scaleFactor;
        //Vector2 pos;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(inventoryCanvas.transform as RectTransform, Input.mousePosition, inventoryCanvas.worldCamera, out pos);
        //dragImageRT.position = inventoryCanvas.transform.TransformPoint(pos);
    }

    // 드래그 종료
    public void OnEndDrag(PointerEventData eventData)
    {
        // 시작 부분에서 아이템이 없었다면 종료, Swap후에 Item이 null이 될 수 있어 Item으로 검사하지 않음
        if (mbBeginDragNull == true)
        {
            mbBeginDragNull = false;
            return;
        }
        
        mItemRectTransform.anchoredPosition = Vector2.zero;
        transform.GetChild(0).GetComponent<Image>().enabled = true;

        // 드래그 이미지 비활성화
        mDragImageRT.GetComponent<Image>().sprite = null;
        mDragImageRT.gameObject.SetActive(false);

        // 레이캐스트 다시 켬
        GetComponent<Image>().raycastTarget = true;
    }

    // 드랍 당할 시(드랍 되는 부분의 슬롯에서 호출)
    public void OnDrop(PointerEventData eventData)
    {
        // 드랍되는 곳에 오브젝트가 존재하지 않거나
        // 해당 오브젝트의 아이템 슬롯이 없거나
        // 아이템 슬롯에 아이템이 없으면 종료
        if (eventData.pointerDrag == null
            || eventData.pointerDrag.GetComponent<ItemSlot>() == null
            || eventData.pointerDrag.GetComponent<ItemSlot>().Item == null)
        {
            return;
        }
        
        // 아이템 조합
        ItemSlot itemSlot = eventData.pointerDrag.GetComponent<ItemSlot>();
        if (Item != null)
        {
            for (int i = 0; i < Item.CombineItemNames.Length; ++i)
            {
                if (itemSlot.Item.ItemName.Equals(mItem.CombineItemNames[i]))
                {
                    Item = mItem.ResultItems[i]; // 조합
                    itemSlot.Item = null;   // 드래그 시작한 곳은 없어지도록 만듬
                    //mParticle.Play();    // 파티클 효과 재생

                    Inventory.Instance.DeactivateItemPopUp();

                    if (IsSelected == true)
                    {
                        IsSelected = false;
                    }
                    else if (itemSlot.IsSelected == true)
                    {
                        itemSlot.IsSelected = false;
                    }

                    // OnDrop이여서 Item의 경우 놓는 곳의 Item을 가지는데 Sort시 다른 곳으로 가버려서
                    // Item이 널로 설정되므로 이를 해결하기 위해 미리 Item의 이름을 저장해두고 이를 이용해
                    // 인덱스를 찾은 후 그곳에 있는 파티클을 재생
                    string itemName = Item.name;    
                    Inventory.Instance.SortSlot();
                    int index = Inventory.Instance.GetItemIndex(itemName);
                    Inventory.Instance.ItemSlots[index].Particle.Play();

                    return;
                }
            }
        }

        // 슬롯끼리 아이템 변경
        SwapSlot(itemSlot);
        Inventory.Instance.SortSlot();
    }

    private void Awake()
    {
        // GetChild이므로 하이어라키 순서가 변경되면 문제 발생
        mDefaultImage = transform.GetChild(0).GetComponent<Image>().sprite;
        mParticle = transform.GetChild(1).GetComponent<ParticleSystem>();
        mSlotImage = GetComponent<Image>();
        mOriginalColor = mSlotImage.color;

        mRectTransform = GetComponent<RectTransform>();
        mItemRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        mInventoryCanvas = Inventory.Instance.GetComponent<Canvas>();
    }

    private void Start()
    {
        // Inventory에서 부모 변경해주므로 Start에서 드래그 시 이미지의 RectTransform을 찾음
        mDragImageRT = transform.parent.Find("DragImage").GetComponent<RectTransform>();
    }

    private void SwapSlot(ItemSlot otherSlot)
    {
        ItemLSH tempItem = Item;
        Item = otherSlot.Item;
        otherSlot.Item = tempItem;
    }
}
