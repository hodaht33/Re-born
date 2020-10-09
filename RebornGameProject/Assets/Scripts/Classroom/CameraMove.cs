using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
