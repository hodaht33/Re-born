using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 아이템 정보 프리팹(데이터)관리
/// 수정자 : 곽진성
/// 수정 : 씬이 아닌 스크립트 내에서 아이템 관리
/// </summary>
public class ItemManager : SingletonBase<ItemManager>
{
    private ItemLSH[] itemList;

    // 해당 아이템이 존재하면 반환하는 메서드
    public ItemLSH GetItem(string itemName)
    {
        foreach(ItemLSH itemLSH in itemList)
        {
            if (itemLSH.ItemName == itemName)
            {
                return itemLSH;
            }
        }

        return null;
    }

    protected override void Awake()
    {
        base.Awake();

        // 아이템 리스트 불러오기
        itemList = Resources.LoadAll<ItemLSH>("Items");
    }
}