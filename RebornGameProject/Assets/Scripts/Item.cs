using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;           // 아이템의 타입.
    [SerializeField]
    private int itemID;
    public int ItemID
    {
        get { return itemID; }
        private set { }
    }
    [SerializeField]
    private Sprite DefaultImg;   // 기본 이미지.
    public Sprite defaultImg
    {
        get { return DefaultImg; }
        private set { }
    }

    [SerializeField]
    private Sprite LargeImg;     // 확대 이미지
    public Sprite largeImg
    {
        get { return LargeImg; }
        private set { }
    }

    //public string sName = null;
    //public int sID = -1;
    //public Sprite sImg = null;
    //public int pID = -1;

    // 인벤토리에 접근하기 위한 변수.
    private Inven Iv;

    [SerializeField]
    private bool isDeactivateItem;


    public Item(string itemName, int itemID, Sprite DefaultImg)
    {
        this.itemName = itemName;
        this.itemID = itemID;
        this.DefaultImg = DefaultImg;
        //sName = null;
        //sID = -1;
        //sImg = null;
        //pID = -1;
    }

    void Awake()
    {
        // 태그명이 "Inventory"인 객체의 GameObject를 반환한다.
        // 반환된 객체가 가지고 있는 스크립트를 GetComponent를 통해 가져온다.
        Iv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inven>();

    }

    void AddItem()
    {
        // 설정 창이 열려있지 않으면 아이템 획득
        if (UIManager.Instance.IsActivateSettings == false)
        //if (TreeQuestionSystem.Instance.Success == true)
        {
            // 아이템 획득에 실패할 경우.
            if (!Iv.AddItem(this))
            {
                Debug.Log("아이템이 가득 찼습니다.");
            }
            else // 아이템 획득에 성공할 경우.
            {
                if (isDeactivateItem == false)
                {
                    gameObject.SetActive(false); // 아이템을 비활성화 시켜준다.
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
            AddItem();
    }

    /*private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit);

            if (hit.collider != null)
            {
                AddItem();
            }
        }
    }*/

    private void OnMouseDown()
    {
        AddItem();
    }
}
