using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 아이템 정보 프리팹(데이터)관리
/// </summary>
public class ItemManager : SingletonBase<ItemManager>
{
    public ItemLSH GetItem(string itemName)
    {
        ItemLSH item = transform.Find(itemName).GetComponent<ItemLSH>();
        
        if (item != null)
        {
            return item;
        }

        return null;
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
