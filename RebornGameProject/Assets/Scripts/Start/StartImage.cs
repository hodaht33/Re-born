using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartImage : MonoBehaviour
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
}
