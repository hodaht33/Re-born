using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml.Serialization;

/// <summary>
/// 삭제 예정
/// </summary>
public class Inven : MonoBehaviour
{
    private List<GameObject> allSlot = new List<GameObject>();    // 모든 슬롯을 관리해줄 리스트.
    public List<GameObject> AllSlot
    {
        get { return allSlot; }
        private set { }
    }

    [SerializeField]
    private RectTransform InvenRect;     // 인벤토리의 Rect
    [SerializeField]
    private GameObject OriginSlot;       // 오리지널 슬롯.

    [SerializeField]
    private float slotSize;              // 슬롯의 사이즈.
    [SerializeField]
    private float slotGap;               // 슬롯간 간격.
    [SerializeField]
    private float slotCountX;            // 슬롯의 가로 개수.

    private float InvenWidth;           // 인벤토리 가로길이.
    private float InvenHeight;          // 인밴토리 세로길이.
    private float EmptySlot;            // 빈 슬롯의 개수.

    //private int i = 0;

    void Awake()
    {
        // 인벤토리 이미지의 가로, 세로 사이즈 셋팅.
        InvenWidth = transform.gameObject.GetComponent<RectTransform>().rect.width;
        InvenHeight = transform.gameObject.GetComponent<RectTransform>().rect.height;

        for (int x = 0; x < slotCountX; x++)
        {
            // 슬롯을 복사한다.
            GameObject slot = Instantiate(OriginSlot) as GameObject;
            // 슬롯의 RectTransform을 가져온다.
            RectTransform slotRect = slot.GetComponent<RectTransform>();
            // 슬롯의 자식인 투명이미지의 RectTransform을 가져온다.
            RectTransform item = slot.transform.GetChild(0).GetComponent<RectTransform>();

            slot.name = "slot_" + x; // 슬롯 이름 설정.
            slot.transform.SetParent(transform); // 슬롯의 부모를 설정. (Inventory객체가 부모임.)

            // 슬롯이 생성될 위치 설정하기.
            slotRect.localPosition = new Vector3((float)-245 + (slotSize + slotGap) * x , 0, 0);

            // 슬롯의 자식인 투명이미지의 사이즈 설정하기.
            slotRect.localScale = Vector3.one;
            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize); // 가로
            slotRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize);   // 세로.

            // 슬롯의 사이즈 설정하기.
            item.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, slotSize - slotSize * 0.3f); // 가로.
            item.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, slotSize - slotSize * 0.3f);   // 세로.

            // 리스트에 슬롯을 추가.
             allSlot.Add(slot);
        }
        GameObject.FindGameObjectWithTag("DragImg").transform.SetParent(transform);
        GameObject.FindGameObjectWithTag("DragImg").SetActive(false);

        //GameObject.FindGameObjectWithTag("LargeImg").transform.parent = transform;
        //GameObject.FindGameObjectWithTag("LargeImg").SetActive(false);

        // 빈 슬롯 = 슬롯의 숫자.
        EmptySlot = allSlot.Count;
        //Invoke("Init", 0.01f);

        OriginSlot.SetActive(false);
    }

    void Init()
    {
        //ItemIO.Load(AllSlot);
    }

    // 아이템을 넣기위해 모든 슬롯을 검사.
    public bool AddItem(Item item)
    {
        // 슬롯에 총 개수.
        int slotCount = allSlot.Count;
        
        // 빈 슬롯에 아이템을 넣기위한 검사.
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = allSlot[i].GetComponent<Slot>();

            // 슬롯이 비어있지 않으면 통과
            if (slot.isSlots())
                continue;

            slot.AddItem(item);
            return true;
        }

        // 위에 조건에 해당되는 것이 없을 때 아이템을 먹지 못함.
        return false;
    }

    // 거리가 가까운 슬롯의 반환.
    public Slot NearDisSlot(Vector3 Pos)
    {
        float Min = 10000f;
        int Index = -1;

        int Count = allSlot.Count;
        for (int i = 0; i < Count; i++)
        {
            Vector2 sPos = allSlot[i].transform.GetChild(0).position;
            float Dis = Vector2.Distance(sPos, Pos);

            if (Dis < Min)
            {
                Min = Dis;
                Index = i;
            }
        }

        if (Min > slotSize)
            return null;

        return allSlot[Index].GetComponent<Slot>();
    }

    // 아이템 옮기기 및 교환.
    public void Swap(Slot slot, Vector3 Pos)
    {
        Slot FirstSlot = NearDisSlot(Pos);

        if (slot.DefaultImg == slot.ItemImg)
            slot.transform.GetChild(0).gameObject.SetActive(false);

        // 현재 슬롯과 옮기려는 슬롯이 같으면 함수 종료.
        if (slot == FirstSlot || FirstSlot == null)
        {
            slot.transform.GetChild(0).gameObject.SetActive(true);
            slot.UpdateInfo(true, slot.Slots.Peek().defaultImg);
            return;
        }
        else
            slot.transform.GetChild(0).gameObject.SetActive(false);

        // 가까운 슬롯이 비어있으면 옮기기.
        if (!FirstSlot.isSlots())
        {
            Swap(FirstSlot, slot);
        }
        // 교환.
        //else if (slot.slot.Peek().pID == FirstSlot.slot.Peek().itemID && slot.slot.Peek().pID != -1)
        //{
            //FirstSlot.slot.Clear();
            //SynSwap(FirstSlot, slot);
        //}
        else
        {
            int Count = slot.Slots.Count;
            Item item = slot.Slots.Peek();
            Stack<Item> temp = new Stack<Item>();

            {
                for (int i = 0; i < Count; i++)
                    temp.Push(item);

                slot.Slots.Clear();
            }

             Swap(slot, FirstSlot);

            {
                Count = temp.Count;
                item = temp.Peek();

                for (int i = 0; i < Count; i++)
                    FirstSlot.Slots.Push(item);

                FirstSlot.UpdateInfo(true, temp.Peek().defaultImg);
            }
        }
    }

    // 1: 비어있는 슬롯, 2: 안 비어있는 슬롯.
    void Swap(Slot xFirst, Slot oSecond)
    {
        int Count = oSecond.Slots.Count;
        Item item = oSecond.Slots.Peek();

        for (int i = 0; i < Count; i++)
        {
            if (xFirst != null)
                xFirst.Slots.Push(item);
        }

        if (xFirst != null)
        {
            xFirst.transform.GetChild(0).gameObject.SetActive(true);
            xFirst.UpdateInfo(true, oSecond.ItemReturn().defaultImg);
        }

        oSecond.Slots.Clear();
        oSecond.UpdateInfo(false, oSecond.DefaultImg);
    }

    // 1: 비어있는 슬롯, 2: 안 비어있는 슬롯.
    /*void SynSwap(Slot xFirst, Slot oSecond)
    {
        int Count = oSecond.slot.Count;
        Item item = new Item(oSecond.slot.Peek().sName, oSecond.slot.Peek().sID, oSecond.slot.Peek().sImg);

        for (int i = 0; i < Count; i++)
        {
            if (xFirst != null)
                xFirst.slot.Push(item);
        }

        if (xFirst != null)
        {
            xFirst.transform.GetChild(0).gameObject.SetActive(true);
            xFirst.UpdateInfo(true, oSecond.ItemReturn().DefaultImg);
        }

        oSecond.slot.Clear();
        //oSecond.ItemUse();
        //oSecond.AddItem(item);
        oSecond.UpdateInfo(false, oSecond.DefaultImg);
    }*/
}
