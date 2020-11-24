using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 연필 사용하는 문제 기능 아마도 삭제!
/// </summary>

public class clip : MonoBehaviour
{
    [SerializeField]
    private Sprite mExistImg;
    [SerializeField]
    private Sprite mImg;

    public void onClick()
    {
        if (gameObject.GetComponent<Image>().sprite != mExistImg)
        {
            return;
        }

        GameObject Inven = GameObject.Find("Inventory").gameObject;
        List<GameObject> AllSlot = Inven.GetComponent<Inven>().AllSlot;
        // 슬롯에 총 개수.
        int slotCount = AllSlot.Count;

        // 빈 슬롯에 아이템을 넣기위한 검사.
        for (int i = 0; i < slotCount; i++)
        {
            Slot s = AllSlot[i].GetComponent<Slot>();

            // 슬롯이 비어있지 않으면 통과
            if (s.mbChoice == true && 
                s.ItemReturn().GetComponent<Item>().ItemID == 4)
            {
                GameObject.Find("clipboard").gameObject.GetComponent<LargeImage>().LargeImg = mImg;

                GameObject large = GameObject.Find("Canvas").transform.Find("LargeImg").gameObject;
                large.transform.GetComponent<Image>().sprite = mImg;

                s.ItemUse();
                s.mbChoice = false;
            }

        }
    }
}
