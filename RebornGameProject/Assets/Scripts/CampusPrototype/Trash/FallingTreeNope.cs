using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTreeNope : MonoBehaviour
{
    private Vector3 dir;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float fallRiseSpeed = 20.0f;
    private Coroutine curr = null;

    private void Awake()
    {
        Vector3 targetDirection = transform.parent.Find("Direction").position;
        Vector3 targetDir = (targetDirection - transform.position).normalized;
        dir = Vector3.Cross(Vector3.up, targetDir);
    }

    private void Update()
    {
        //transform.Rotate(dir * Time.deltaTime * speed);

        if (Input.GetKey(KeyCode.W))
        {
            if (curr != null)
            {
                StopCoroutine(curr);
            }
            curr = StartCoroutine(Falling());
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (curr != null)
            {
                StopCoroutine(curr);
            }
            curr = StartCoroutine(RiseUp());
        }
    }

    private IEnumerator Falling()
    {
        float fallingSpeed = speed;
        float prevAngleX = 0.0f;
        while (transform.eulerAngles.x - prevAngleX >= 0.0f)
        {
            prevAngleX = transform.eulerAngles.x;
            fallingSpeed += Time.deltaTime * fallRiseSpeed;
            transform.Rotate(dir * Time.deltaTime * fallingSpeed);

            yield return null;
        }

        //transform.eulerAngles = new Vector3(90.0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private IEnumerator RiseUp()
    {
        float riseUpSpeed = speed;

        while (180.0f - transform.eulerAngles.x > 0.0f)
        {
            riseUpSpeed += Time.deltaTime * fallRiseSpeed;
            transform.Rotate(dir * Time.deltaTime * riseUpSpeed);

            yield return null;
        }

        //transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
