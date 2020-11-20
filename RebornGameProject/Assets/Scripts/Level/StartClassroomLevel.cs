using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartClassroomLevel : MonoBehaviour
{
    [SerializeField] private Transform playerStartPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true
            && Inventory.Instance.FindItem("Key") == true)
        {
            Camera.main.GetComponent<TreeQuestionCamera>().enabled = false;
            Camera.main.GetComponent<ClassroomCameraMove>().enabled = true;
            Camera.main.cullingMask -= 1 << LayerMask.NameToLayer("Level2");
            Camera.main.cullingMask += 1 << LayerMask.NameToLayer("Level3");
            other.transform.Rotate(Vector3.up * -90.0f);
            other.transform.position = playerStartPos.position;
        }
    }
}
