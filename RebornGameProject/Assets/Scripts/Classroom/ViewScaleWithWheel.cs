using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewScaleWithWheel : MonoBehaviour
{
    private float scroll;

    [SerializeField]
    private float speed = 5.0f;

    private Camera camera;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    private void Update()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel") * speed;

        //camera.fieldOfView -= scroll;

        //if (camera.fieldOfView <= 30.0f && scroll > 0.0f)
        //{
        //    camera.fieldOfView = 30.0f;
        //}
        //else if (camera.fieldOfView >= 60.0f && scroll < 0.0f)
        //{
        //    camera.fieldOfView = 60.0f;
        //}

        if (scroll != 0.0f)
        {
            transform.Translate(Vector3.forward * scroll * speed);
        }
    }
}
