using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// 
/// 작성자 : 이성호
/// 기능 : 옆으로 밀리는 자동문
/// </summary>
public class Door_Slide : MonoBehaviour
{
    private Transform hinge;
    private bool isOpen;

    private Vector3 dest;

    private void Awake()
    {
        hinge = transform.parent;

        dest = new Vector3(transform.position.x, transform.position.y, transform.position.z + 3.0f);
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
        //Vector3 angle = hinge.eulerAngles;
        //while (angle.y < 90.0f)
        //{
        //    angle.y += Time.deltaTime * 100.0f;
        //    hinge.eulerAngles = angle;

        //    yield return null;
        //}

        while (transform.position.z < dest.z)
        {
            transform.position = Vector3.Lerp(transform.position, dest, Time.deltaTime * 2.0f);
            yield return null;
        }
    }
}
