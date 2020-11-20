using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassroomCameraMove : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;

    private void Awake()
    {
        target = target1;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 5.0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.RotateTowards(transform.rotation, target.rotation, 10.0f), Time.deltaTime * 10.0f);
    }
}
