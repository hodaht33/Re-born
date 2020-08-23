using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Item> inventory = new List<Item>();
    private ItemDatabase db;
    private GameObject dbObject;
    public int slotX, slotY;
    public List<Item> slots = new List<Item>();
    private bool showInventory = false;
    public GUISkin skin;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < slotX * slotY; i++)
        {
            slots.Add(new Item());
            inventory.Add(new Item());
        }

        db = GameObject.FindGameObjectWithTag("itemDatabase").GetComponent<ItemDatabase>();

        inventory[0] = db.items[0];
        inventory[1] = db.items[1];

        for (int i = 0;/*db.items[i]!=null*/i < slotX * slotY; i++)
        {
            if (db.items[i] != null)
            {
                inventory[i] = db.items[i];
                // 디비의 아이템칸에 비어있지 않다면, 저장
            }
            else
            {
                // 디비의 아이템칸이 비어있다면 다른 행동을 하도록 유도합니다.
            }
        }

        /* for(int i=0; i<n; i++) {
         *      inventory.Add(db.items[i]);
         * }
         */
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            showInventory = !showInventory;
        }
    }

    private void OnGUI()
    {
        GUI.skin = skin;

        if (showInventory)
        {
            DrawInventory();
        }
    }

    void DrawInventory()
    {
        int k = 0;
        for (int j = 0; j < slotY; j++)
        {

            for (int i = 0; i < slotX; i++)
            {
                Rect slotRect = new Rect(i * 52 + 100, j * 52 + 30, 50, 50);
                // 박스 분할하기
                GUI.Box(slotRect, "", skin.GetStyle("slot background"));
                // 각 박스의 생성 위치를 설정해주는 곳입니다. skin.GetStyle은 이전에 만들었던 skin을 불러오는 것임

                // 기능 추가하기
                slots[k] = inventory[k];
                if (slots[k].itemName != null)
                {
                    GUI.DrawTexture(slotRect, slots[k].itemIcon);
                    Debug.Log(slots[k].itemName);
                }

                k++;
                // 갯수 증가
            }
        }
    }
}
