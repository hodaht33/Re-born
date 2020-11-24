using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
