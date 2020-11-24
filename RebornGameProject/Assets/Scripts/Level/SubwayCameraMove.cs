using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public class SubwayCameraMove : MonoBehaviour
{
    [SerializeField] private Transform moveLimitLeft;
    [SerializeField] private Transform moveLimitRight;
    [SerializeField] private Transform target;
    [SerializeField] private Transform player;
    [SerializeField] private float cameraSpeed = 5.0f;
    private Quaternion defaultRotation;

    private void Awake()
    {
        moveLimitLeft.position = new Vector3(moveLimitLeft.position.x, target.position.y, target.position.z);
        moveLimitRight.position = new Vector3(moveLimitRight.position.x, target.position.y, target.position.z);
        defaultRotation = Camera.main.transform.rotation;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * cameraSpeed);

        if (transform.position.x < moveLimitLeft.position.x)
        {
            transform.position = moveLimitLeft.position;
            //transform.LookAt(player);
            //바라보는 것에 lerp걸어야 자연스러움 - 현재 부자연스러움
        }
        else if (transform.position.x > moveLimitRight.position.x)
        {
            transform.position = moveLimitRight.position;
            //transform.LookAt(player);
        }
        else
        {
            //transform.rotation = Quaternion.Lerp(transform.rotation, defaultRotation, Time.deltaTime * 10.0f);
        }
    }
}
