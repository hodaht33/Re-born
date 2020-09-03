using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenSize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector2 size = new Vector2(Screen.width, Screen.height);
        transform.gameObject.GetComponent<RectTransform>().sizeDelta = size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        gameObject.transform.parent.Find("LargeImg").gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
