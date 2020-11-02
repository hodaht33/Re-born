using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private TextAsset itemData;

    private Dictionary<int, ItemFormat> dicItems = new Dictionary<int, ItemFormat>();
    public Dictionary<int, ItemFormat> Items
    {
        get { return dicItems; }
    }

    private void Awake()
    {
        string[] datas = itemData.text.Split('\n');
        
        for (int i = 1; i < datas.Length; ++i)
        {
            string[] rowData = datas[i].Split(',');

            if (rowData[0] == "")
            {
                break;
            }

            string[] combineNumberString = rowData[5].Split('.');
            int[] combineNumbers = new int[combineNumberString.Length];
            for (int j  = 0; j < combineNumbers.Length; ++j)
            {
                combineNumbers[j] = int.Parse(combineNumberString[j]);
            }

            string[] resultNumberString = rowData[6].Split('.');
            int[] resultNumbers = new int[resultNumberString.Length];
            for (int j = 0; j < resultNumbers.Length; ++j)
            {
                resultNumbers[j] = int.Parse(resultNumberString[j]);
            }

            dicItems.Add(int.Parse(rowData[1]),
                new ItemFormat
                (
                    rowData[0],
                    int.Parse(rowData[1]),
                    rowData[2],
                    rowData[3],
                    rowData[4] == "TRUE",
                    combineNumbers,
                    resultNumbers
                )
            );
        }
    }

    public ItemLSH GetItem(int itemNumber)
    {
        //return dicItems[itemNumber];
        return null;
    }
}
