using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveL : MonoBehaviour
{
    [SerializeField]
    private Transform camPos;

    [SerializeField]
    private float camSpeed = 5.0f;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, camPos.position, Time.deltaTime * camSpeed);
    }
}
