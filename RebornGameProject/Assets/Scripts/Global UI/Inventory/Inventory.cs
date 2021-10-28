#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 인벤토리 관리
/// </summary>
public class Inventory : SingletonBase<Inventory>
{
    private List<ItemSlot> mItemSlots;   // 아이템 슬롯 관리 리스트
    public List<ItemSlot> ItemSlots
    {
        get
        {
            return mItemSlots;
        }
    }

    private RectTransform mItemPanel;
    private ItemSlot mSelectedSlot;

    private GraphicRaycaster mGraphicRaycaster;
    private PointerEventData mPointerEventData;
    private List<RaycastResult> mRaycastResults;

    [SerializeField]
    private Canvas mItemPopUpCanvas;
    private Image mPopUpImage;

    [SerializeField]
    private RectTransform mInventoryTransform;
    [SerializeField, Range(1.0f, 100.0f)]
    private float mInventoryMoveTime = 5.0f;
    private Vector2 mInventoryDefaultPos;
    private Vector2 mInventoryHidePos;

    // 인벤토리 화살표 표시
    [SerializeField]
    private Image arrow;
    [SerializeField]
    private Sprite upArrow;
    [SerializeField]
    private Sprite downArrow;

    private Coroutine mCurrentCoroutine;
    public Coroutine CurrentCoroutine
    {
        get { return mCurrentCoroutine; }
        set { mCurrentCoroutine = value; }
    }

    [SerializeField, Range(1.0f, 10.0f)]
    private float mWaitTime = 2.0f;
    private WaitForSeconds mWaitForSeconds;

    private InventoryMouse mInventoryMouse;

    private Coroutine mTickCoroutine;
    [SerializeField]
    private float mActivateTime = 10.0f;

    // 코루틴 중지 메서드
    public void StopCoroutineInline()
    {
        if (CurrentCoroutine != null)
        {
            StopCoroutine(CurrentCoroutine);
            CurrentCoroutine = null;
        }
    }

    // 아이템 팝업 비활성화 메서드
    public void DeactivateItemPopUp()
    {
        mItemPopUpCanvas.enabled = false;
    }

    // 마우스 버튼 클릭 이벤트
    public void MouseUp()
    {
        mPointerEventData.position = Input.mousePosition;    // 이벤트 발생 위치를 마우스 위치로 지정
        mRaycastResults.Clear(); // 레이캐스팅 이전 결과 초기화
        mGraphicRaycaster.Raycast(mPointerEventData, mRaycastResults); // UI 레이캐스팅

        // 레이캐스팅 된 UI가 없거나
        // ItemSlot을 가진 UI가 아니거나
        // ItemSlot에 아이템을 가지지 않은 경우 함수 종료
        if (mRaycastResults.Count == 0
            || mRaycastResults[0].gameObject.GetComponent<ItemSlot>() == null
            || mRaycastResults[0].gameObject.GetComponent<ItemSlot>().Item == null)
        {
            return;
        }

        ItemSlot resultSlot = mRaycastResults[0].gameObject.GetComponent<ItemSlot>();

        // 팝업 창 띄우기
        if (Chat.Instance.IsChatActivated == false)
        {
            mTickCoroutine = StartCoroutine(TickActivateTimeCoroutine());
            mItemPopUpCanvas.enabled = true;
            mPopUpImage.sprite = resultSlot.Item.Sprite;
        }

        #region 슬롯선택
        // 다른 슬롯이 이미 선택되었는지 검사하는 반복문
        foreach (ItemSlot slot in mItemSlots)
        {
            // 자기 자신은 검사 대상에서 제외
            if (slot.Equals(resultSlot) == true)
            {
                continue;
            }

            // 다른 슬롯이 이미 선택되어 있을 경우
            if (slot.IsSelected == true)
            {
                // 선택되었었던 슬롯 비활성화
                slot.IsSelected = false;

                // 클릭한 슬롯 활성화
                resultSlot.IsSelected = true;
                resultSlot.GetItemEvent();

                mSelectedSlot = resultSlot;

                return;  // 어차피 하나만 활성화 중 일 것이므로 함수 종료
            }
        }

        if (resultSlot.IsSelected == true)
        {
            mSelectedSlot = null;
            resultSlot.IsSelected = false;

            DeactivateItemPopUp();
        }
        else
        {
            mSelectedSlot = resultSlot;
            resultSlot.IsSelected = true;
            if(resultSlot.GetItemEvent != null)
                resultSlot.GetItemEvent();
        }
        #endregion
    }

    // 아이템 획득
    public ItemSlot GetItem(ItemLSH item)
    {
        for (int i = 0; i < 5; ++i)
        {
            if (mItemSlots[i].Item == null)
            {
                mItemSlots[i].Item = item;

                return mItemSlots[i];
            }
        }

        return null;
    }

    // 아이템 사용
    public bool UseItem(string itemName)
    {
        for (int i = 0; i < mItemSlots.Count; ++i)
        {
            if (mItemSlots[i].Item != null && itemName.Equals(mItemSlots[i].Item.ItemName) == true)
            {
                mItemSlots[i].Item = null;
                mItemSlots[i].IsSelected = false;
                DeactivateItemPopUp();
                StartAndGetCoroutineUpAndDownInventory();

                SortSlot();

                return true;
            }
        }

        return false;
    }

    // 선택되어 있는 슬롯의 아이템 사용
    public bool UseSelectedItem(string itemName)
    {
        // 슬롯이 선택되어 있지 않거나 아이템이 없다면 거짓 반환
        if (mSelectedSlot == null
            || mSelectedSlot.Item == null)
        {
            return false;
        }

        // 맞는 아이템 사용
        if (itemName.Equals(mSelectedSlot.Item.ItemName) == true)
        {
            mSelectedSlot.IsSelected = false;
            mSelectedSlot.Item = null;
            mSelectedSlot = null;
            DeactivateItemPopUp();
            StartAndGetCoroutineUpAndDownInventory();

            SortSlot();

            return true;
        }

        return false;
    }

    // 아이템 찾기
    public bool FindItem(string itemName)
    {
        for (int i = 0; i < mItemSlots.Count; ++i)
        {
            if (mItemSlots[i].Item == null)
            {
                continue;
            }

            if (itemName.Equals(mItemSlots[i].Item.ItemName) == true)
            {
                return true;
            }
        }

        return false;
    }

    // 아이템 찾아 인덱스 반환
    public int GetItemIndex(string itemName)
    {
        for (int i = 0; i < mItemSlots.Count; ++i)
        {
            if (mItemSlots[i].Item == null)
            {
                continue;
            }

            if (itemName.Equals(mItemSlots[i].Item.ItemName) == true)
            {
                return i;
            }
        }

        return -1;
    }

    // 마우스가 패널에 들어와 올라가도록 구현
    public IEnumerator UpInventoryCoroutine()
    {
        arrow.sprite = downArrow;

        while (mInventoryTransform.anchoredPosition.y < mInventoryDefaultPos.y - 0.5f)
        {
            mInventoryTransform.anchoredPosition = Vector2.Lerp(mInventoryTransform.anchoredPosition, mInventoryDefaultPos, Time.deltaTime * mInventoryMoveTime);

            yield return null;
        }

        mInventoryTransform.anchoredPosition = mInventoryDefaultPos;
    }

    // 마우스가 패널 위치에서 벗어나 내려가도록 구현
    public IEnumerator DownInventoryCoroutine()
    {
        arrow.sprite = upArrow;

        while (mInventoryTransform.anchoredPosition.y > mInventoryHidePos.y + 0.5f)
        {
            mInventoryTransform.anchoredPosition = Vector2.Lerp(mInventoryTransform.anchoredPosition, mInventoryHidePos, Time.deltaTime * mInventoryMoveTime);

            yield return null;
        }

        mInventoryTransform.anchoredPosition = mInventoryHidePos;
    }

    // 아이템 획득 시 자동으로 위로 올라왔다가 몇 초 후 아래로 내려가도록 구현
    public IEnumerator UpAndDownInventoryCoroutine()
    {
        //inventoryMouse.enabled = false;

        arrow.sprite = downArrow;

        while (mInventoryTransform.anchoredPosition.y < mInventoryDefaultPos.y - 0.5f)
        {
            mInventoryTransform.anchoredPosition = Vector2.Lerp(mInventoryTransform.anchoredPosition, mInventoryDefaultPos, Time.deltaTime * mInventoryMoveTime);

            yield return null;
        }

        mInventoryTransform.anchoredPosition = mInventoryDefaultPos;

        yield return mWaitForSeconds;

        arrow.sprite = upArrow;

        while (mInventoryTransform.anchoredPosition.y > mInventoryHidePos.y + 0.5f)
        {
            mInventoryTransform.anchoredPosition = Vector2.Lerp(mInventoryTransform.anchoredPosition, mInventoryHidePos, Time.deltaTime * mInventoryMoveTime);

            yield return null;
        }

        mInventoryTransform.anchoredPosition = mInventoryHidePos;

        //inventoryMouse.enabled = true;
    }

    // 인벤토리 위로 나오게하는 코루틴 실행 메서드
    public Coroutine StartAndGetCoroutineUpInventory()
    {
        StopCoroutineInline();

        return CurrentCoroutine = StartCoroutine(UpInventoryCoroutine());
    }

    // 인벤토리 아래로 내려가게하는 코루틴 실행 메서드
    public Coroutine StartAndGetCoroutineDownInventory()
    {
        StopCoroutineInline();

        return CurrentCoroutine = StartCoroutine(DownInventoryCoroutine());
    }

    // 인벤토리 위로 이동 후 알아서 아래로 내려가게하는 코루틴 실행 메서드
    public Coroutine StartAndGetCoroutineUpAndDownInventory()
    {
        StopCoroutineInline();

        return CurrentCoroutine = StartCoroutine(UpAndDownInventoryCoroutine());
    }

    // 아이템들이 왼쪽부터 차례대로 들어가 있도록 정렬하는 메서드
    public void SortSlot()
    {
        for (int i = 0; i < mItemSlots.Count; ++i)
        {
            if (mItemSlots[i].Item != null)
            {
                continue;
            }

            for (int j = i + 1; j < mItemSlots.Count; ++j)
            {
                if (mItemSlots[j].Item != null)
                {
                    mItemSlots[i].Item = mItemSlots[j].Item;
                    mItemSlots[j].Item = null;

                    break;
                    //mItemSlots[i].SlotImage = mItemSlots[j].SlotImage;
                    //mItemSlots[j].SlotImage = null;
                }
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();

        mItemSlots = new List<ItemSlot>(5);
        mItemPanel = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();    // 0번째를 원본으로 복사본 생성
        mGraphicRaycaster = GetComponent<GraphicRaycaster>();
        mPointerEventData = new PointerEventData(null);
        mRaycastResults = new List<RaycastResult>();

        mItemSlots.Add(mItemPanel.GetComponent<ItemSlot>());

        // 슬롯 추가
        for (int i = 1; i < mItemSlots.Capacity; ++i)
        {
            RectTransform panel = Instantiate(mItemPanel, transform.GetChild(0));
            panel.anchoredPosition = new Vector2(panel.anchoredPosition.x + 150 * i, panel.anchoredPosition.y);
            mItemSlots.Add(panel.GetComponent<ItemSlot>());
        }

        transform.Find("DragImage").SetParent(transform.Find("InventoryPanel"));

        mPopUpImage = mItemPopUpCanvas.transform.Find("PopUpImage").GetComponent<Image>();

        mInventoryDefaultPos = mInventoryTransform.anchoredPosition;
        mInventoryHidePos = new Vector2(mInventoryDefaultPos.x, mInventoryDefaultPos.y - 200.0f);

        mInventoryTransform.anchoredPosition = mInventoryHidePos;

        mWaitForSeconds = new WaitForSeconds(mWaitTime);

        mInventoryMouse = FindObjectOfType<InventoryMouse>();
    }

    private IEnumerator TickActivateTimeCoroutine()
    {
        float tickTime = 0.0f;

        while (tickTime <= mActivateTime)
        {
            tickTime = tickTime + Time.deltaTime;

            yield return null;
        }

        DeactivateItemPopUp();
    }
}