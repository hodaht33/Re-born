using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LargeImage : MonoBehaviour
{
    public Sprite LargeImg;

    private void OnMouseDown()
    {
        //Debug.Log("largeImg");
        GameObject large = GameObject.Find("Canvas").transform.Find("LargeImg").gameObject;

        if (large.activeSelf == false)
        {
            large.SetActive(true);
            large.transform.GetComponent<Image>().sprite = LargeImg;
            GameObject.Find("Canvas").transform.Find("back_gray").gameObject.SetActive(true);
        }
    }
}
