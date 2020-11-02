using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Inventory inventory;
    private Transform hinge;
    private bool isOpen;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        hinge = transform.parent;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (isOpen == false
            && other.CompareTag("Player")
            && inventory.UseItem("Key"))
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
