using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class clip : MonoBehaviour
{
    public Sprite existImg;
    public Sprite img;

    public void onClick()
    {
        if (gameObject.GetComponent<Image>().sprite != existImg)
            return;

        GameObject Inven = GameObject.Find("Inventory").gameObject;
        List<GameObject> AllSlot = Inven.GetComponent<Inven>().AllSlot;
        // 슬롯에 총 개수.
        int slotCount = AllSlot.Count;

        // 빈 슬롯에 아이템을 넣기위한 검사.
        for (int i = 0; i < slotCount; i++)
        {
            Slot s = AllSlot[i].GetComponent<Slot>();

            // 슬롯이 비어있지 않으면 통과
            if (s.choice == true && s.ItemReturn().GetComponent<Item>().itemID == 4)
            {
                GameObject.Find("clipboard").gameObject.GetComponent<LargeImage>().LargeImg = img;

                GameObject large = GameObject.Find("Canvas").transform.FindChild("LargeImg").gameObject;
                large.transform.GetComponent<Image>().sprite = img;

                s.ItemUse();
                s.choice = false;
            }

        }
    }
}
