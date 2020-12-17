using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField, Range(2.0f, 20.0f)]
    private float rotateSpeed = 5.0f;
    private float yAxis;


    private void Update()
    {
        yAxis = Input.GetAxis("Mouse X") * rotateSpeed;
        transform.Rotate(0, yAxis, 0);
    }

    private IEnumerator RotateToFront()
    {


        yield return null;
    }

    private IEnumerator RotateToBack()
    {


        yield return null;
    }
}
