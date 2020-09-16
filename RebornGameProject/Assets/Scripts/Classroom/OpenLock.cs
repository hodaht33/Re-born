using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLock : MonoBehaviour
{
    private void OnMouseDown()
    {
        //Debug.Log("largeImg");
        GameObject lockObject = GameObject.Find("Canvas").transform.FindChild("lock").gameObject;

        if (lockObject.activeSelf == false)
        {
            lockObject.SetActive(true);
            GameObject.Find("Canvas").transform.FindChild("back_gray").gameObject.SetActive(true);
        }
    }
}
