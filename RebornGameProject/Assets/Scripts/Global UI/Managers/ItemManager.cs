using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 아이템 정보 프리팹(데이터)관리
/// </summary>
public class ItemManager : SingletonBase<ItemManager>
{
    // 해당 아이템이 존재하면 반환하는 메서드
    public ItemLSH GetItem(string itemName)
    {
        ItemLSH item = transform.Find(itemName).GetComponent<ItemLSH>();

        return item;
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
}
