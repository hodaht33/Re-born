using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenSize : MonoBehaviour
{
    private void Start()
    {
        Vector2 size = new Vector2(Screen.width, Screen.height);
        transform.gameObject.GetComponent<RectTransform>().sizeDelta = size;
    }

    public void onClick()
    {
        gameObject.transform.parent.Find("LargeImg").gameObject.SetActive(false);
        gameObject.SetActive(false);
        gameObject.transform.parent.Find("lock").gameObject.SetActive(false);
    }
}
