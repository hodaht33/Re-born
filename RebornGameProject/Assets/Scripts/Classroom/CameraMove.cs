using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 카메라 고정 (삭제삭제)
/// </summary>

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Transform cam;

    void Start()
    {
        cam = GetComponent<Transform>();
    }

    void LateUpdate()
    {
        cam.position = new Vector3(player.position.x - 0.52f, cam.position.y, cam.position.z);
        //cam.LookAt(player);
    }
}
