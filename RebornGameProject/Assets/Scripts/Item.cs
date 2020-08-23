using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Item
{
    public string itemName;
    public int itemID;
    public string itemDes;
    public Texture2D itemIcon;

    public Item()
    {

    }

    public Item(string itemName, int itemID, string itemDes)//, string img)
    {
        this.itemName = itemName;
        this.itemID = itemID;
        this.itemDes = itemDes;
        //this.itemIcon = Resources.Load<Texture2D>("ItemIcons/34x34icons180709_" + img);
    }
}