using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public class DeactivateInvenItemPopUp : MonoBehaviour
{
    private Canvas itemPopUpCanvas;

    private void Awake()
    {
        itemPopUpCanvas = GetComponent<Canvas>();
    }
    
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        itemPopUpCanvas.enabled = false;
    //        enabled = false;
    //    }
    //}
}
