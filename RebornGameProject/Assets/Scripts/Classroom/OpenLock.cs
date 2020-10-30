using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLock : MonoBehaviour
{
    private void OnMouseDown()
    {
        //Debug.Log("largeImg");
        GameObject lockObject = GameObject.Find("Canvas").transform.Find("lock").gameObject;

        if (lockObject.activeSelf == false)
        {
            lockObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("back_gray").gameObject.SetActive(true);
        }
    }
}
