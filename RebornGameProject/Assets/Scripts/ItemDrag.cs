using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 박서현 아마도?
/// 기능 : 인벤토리에서 아이템 클릭시 기능
/// </summary>


public class ItemDrag : MonoBehaviour
{
    [SerializeField]
    private Transform mImg;   // 빈 이미지 객체.

    private Image mEmptyImg; // 빈 이미지.
    private Slot mSlot;      // 현재 슬롯에 스크립트

    private GameObject mLarge;

    void Start()
    {
        // 현재 슬롯의 스크립트를 가져온다.
        mSlot = GetComponent<Slot>();
        // 빈 이미지 객체를 태그를 이용하여 가져온다.
        //Img = GameObject.FindGameObjectWithTag("DragImg").transform;
        mImg = gameObject.transform.parent.Find("DragImg").transform;
        // 빈 이미지 객체가 가진 Image컴포넌트를 가져온다.
        mEmptyImg = mImg.GetComponent<Image>();

        mImg.gameObject.SetActive(false);

        mLarge = gameObject.transform.parent.parent.Find("LargeImg").gameObject;

    }

    public void Down()
    {
        // 슬롯에 아이템이 없으면 함수종료.
        if (!mSlot.isSlots() ||
            mLarge.activeSelf)
        {
            return;
        }

        // 아이템 사용시.
        if (Input.GetMouseButtonDown(1))
        {
            mSlot.ItemUse();
            return;
        }

        // 빈 이미지 객체를 활성화 시킨다.
        mImg.gameObject.SetActive(true);

        // 빈 이미지의 사이즈를 변경한다.(해상도가 바뀔경우를 대비.)
        float Size = mSlot.transform.GetComponent<RectTransform>().sizeDelta.x;
        mEmptyImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Size);
        mEmptyImg.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Size);

        // 빈 이미지의 스프라이트를 슬롯의 스프라이트로 변경한다.
        mEmptyImg.sprite = mSlot.ItemReturn().defaultImg;
        // 빈 이미지의 위치를 마우스위로 가져온다.
        mImg.transform.position = Input.mousePosition;
        // 슬롯의 아이템 이미지를 없애준다.
        transform.GetChild(0).gameObject.SetActive(false);
        mSlot.UpdateInfo(true, mSlot.DefaultImg);
    }

    public void Drag()
    {
        // isImg플래그가 false이면 슬롯에 아이템이 존재하지 않는 것이므로 함수 종료.
        if (!mSlot.isSlots() ||
            mLarge.activeSelf)
        {
            return;
        }

        mImg.transform.position = Input.mousePosition;
    }

    public void DragEnd()
    {
        // isImg플래그가 false이면 슬롯에 아이템이 존재하지 않는 것이므로 함수 종료.
        if (!mSlot.isSlots() ||
            mLarge.activeSelf)
        {
            return;
        }

        // 싱글톤을 이용해서 인벤토리의 스왑함수를 호출(현재 슬롯, 빈 이미지의 현재 위치.)
        //ObjManager.Call().IV.Swap(slot, Img.transform.position);
        GameObject.Find("Inventory").GetComponent<Inven>().Swap(mSlot, mImg.transform.position);
        //slot = null;
    }

    public void Up()
    {
        // isImg플래그가 false이면 슬롯에 아이템이 존재하지 않는 것이므로 함수 종료.
        if (!mSlot.isSlots() ||
            mLarge.activeSelf)
        {
            return;
        }

        // 빈 이미지 객체 비활성화.
        mImg.gameObject.SetActive(false);
        // 슬롯의 아이템 이미지를 복구 시킨다.
        mSlot.transform.GetChild(0).gameObject.SetActive(true);
        mSlot.UpdateInfo(true, mSlot.Slots.Peek().defaultImg);
    }

    public void onClick()
    {
        if (!mSlot.isSlots())
            return;

        if (mLarge.activeSelf == false)
        {
            mLarge.SetActive(true);
            mLarge.transform.GetComponent<Image>().sprite = mSlot.ItemReturn().largeImg;
            gameObject.transform.parent.parent.Find("back_gray").gameObject.SetActive(true);
        }
        else
        {
            List<GameObject> AllSlot = gameObject.transform.parent.GetComponent<Inven>().AllSlot;
            // 슬롯에 총 개수.
            int slotCount = AllSlot.Count;

            // 빈 슬롯에 아이템을 넣기위한 검사.
            for (int i = 0; i < slotCount; i++)
            {
                Slot s = AllSlot[i].GetComponent<Slot>();

                // 슬롯이 비어있지 않으면 통과
                if (s.mbChoice == true)
                {
                    s.gameObject.GetComponent<Image>().color = new Color(0, 1, 1);
                    s.mbChoice = false;
                }

            }

            gameObject.GetComponent<Image>().color = new Color(1, 0, 0);
            mSlot.mbChoice = true;

        }
    }

    private void FixedUpdate()
    {
        if (mLarge.activeSelf == false)
        {
            gameObject.GetComponent<Image>().color = new Color(0, 1, 1);
            mSlot.mbChoice = false;
        }
        else if (mSlot.mbChoice == false && 
            gameObject.GetComponent<Image>().color == new Color(1, 0, 0))
        {
            gameObject.GetComponent<Image>().color = new Color(0, 1, 1);
        }
    }
}
