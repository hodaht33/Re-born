using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        items.Add(new Item("A", 1, "aaa"));
        items.Add(new Item("B", 2, "bbb"));
        items.Add(new Item("C", 3, "ccc"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
