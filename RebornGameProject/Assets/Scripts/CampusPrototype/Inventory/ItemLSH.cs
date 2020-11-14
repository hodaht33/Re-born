using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 아이템 기능 수행 및 데이터 관리
/// </summary>
public class ItemLSH : MonoBehaviour
{
    // 아이템 이름(스크립트 이름)
    public string ItemName { get { return name; } }

    // 스프라이트 이미지
    [SerializeField]
    private Sprite sprite;
    public Sprite Sprite { get { return sprite; } }

    // 팝업 창 이미지
    //[SerializeField]
    //private Sprite spritePopUp;
    //public Sprite SpritePopUp { get { return spritePopUp; } }

    // 인벤토리 창 이미지
    //[SerializeField]
    //private Sprite spriteInventory;
    //public Sprite SpriteInventory { get { return spriteInventory; } }

    // 아이템 획득 후 이 오브젝트 유지시킬 지 여부
    [SerializeField]
    private bool keepActive;

    // 조합이 가능한 아이템 목록
    [SerializeField]
    private string[] combineItemNames; // 열쇠조각 같이 2개 이상과 조합 가능한 경우(일반 열쇠조각과 2개가 합쳐진 열쇠조각)
    public string[] CombineItemNames
    {
        get { return combineItemNames; }
    }

    // 결과물로 나올 아이템 목록
    [SerializeField]
    private ItemLSH[] resultItems;
    public ItemLSH[] ResultItems
    {
        get { return resultItems; }
    }

    // 아이템을 획득했는 지 여부
    private bool activeGetItem;
    public bool ActiveGetItem
    {
        get { return activeGetItem; }
        set { activeGetItem = value; }
    }

    private void OnMouseDown()
    {
        AddItem();
    }

    private void AddItem()
    {
        // 아이템 획득을 하지 않았으면서 아이템이 획득 되었을 때
        if (ActiveGetItem == true && Inventory.Instance.GetItem(this))
        {
            gameObject.SetActive(keepActive);
            ActiveGetItem = false;
        }
    }
}
