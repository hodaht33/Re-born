using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableItem", menuName = "Item/NewItem")]
public class ScriptableItem : ScriptableObject
{
    // 적용할 프리팹(각각 따로 에디터에서 적용 시 필요 없을 수 있음)
    [SerializeField]
    private GameObject sourceObject;
    
    // 아이템 이름(스크립트 이름)
    public string ItemName { get { return name; } }

    // 아이템 번호
    [SerializeField]
    private int itemId;
    public int ItemId { get { return itemId; } }

    // 팝업 창 내 이미지
    [SerializeField]
    private Sprite spritePopUp;
    public Sprite SpritePopUp { get { return spritePopUp; } }

    // 인벤토리 내 이미지
    [SerializeField]
    private Sprite spriteInventory;
    public Sprite SpriteInventory { get { return spriteInventory; } }

    // 사용 후 사라지는 지 여부
    [SerializeField]
    private bool keepActivate;

    // 조합 가능한 아이템 목록
    [SerializeField]
    private string[] combineItemNames;
    public string[] CombineItemNames { get { return combineItemNames; } }

    // 조합 결과 아이템 목록
    [SerializeField]
    private ScriptableItem[] resultItems;
    public ScriptableItem[] ResultItems { get { return ResultItems; } }

    // 아이템 획득 여부
    private bool activeGetItem;
    public bool ActiveGetItem { get { return activeGetItem; } set { activeGetItem = value; } }

    // 소모품 여부
    [SerializeField]
    private bool isConsumableItem;
    public bool ConsumableItem { get { return isConsumableItem; } }
    
    private void AddItem()
    {
        //if (ActiveGetItem == true && Inventory.Instance.GetItem(this))
        //{
        //    sourceObject.SetActive(keepActivate);
        //    ActiveGetItem = false;
        //}
    }
}
