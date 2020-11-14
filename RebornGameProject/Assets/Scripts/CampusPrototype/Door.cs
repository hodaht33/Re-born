using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 밀려 열리는 문
/// </summary>
public class Door : MonoBehaviour
{
    private Transform hinge;
    private bool isOpen;

    private void Awake()
    {
        hinge = transform.parent;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (isOpen == false
            && other.CompareTag("Player")
            && Inventory.Instance.UseItem("Key"))
        {
            isOpen = true;
            StartCoroutine(Open());
        }
    }

    private void OnMouseUp()
    {
        if (isOpen == false
            && Inventory.Instance.UseSelectedItem("Key"))
        {
            isOpen = true;
            StartCoroutine(Open());
        }
    }

    private IEnumerator Open()
    {
        Vector3 angle = hinge.eulerAngles;
        while (angle.y < 90.0f)
        {
            angle.y += Time.deltaTime * 100.0f;
            hinge.eulerAngles = angle;

            yield return null;
        }
    }
}
