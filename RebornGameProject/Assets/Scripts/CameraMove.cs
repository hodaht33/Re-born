using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform player;
    private Transform cam;
    public float m;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Transform>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        cam.position = new Vector3(player.position.x - 0.52f, cam.position.y, cam.position.z);
        cam.LookAt(player);
    }
}
