using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusProcess : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Transform cameraStartTarget;
    [SerializeField] private Transform cameraLastTarget;
    private Transform cameraTransform;

    [SerializeField] private float cameraMoveSpeed = 5.0f;

    [SerializeField] private Transform leftDoorTransform;
    [SerializeField] private Transform rightDoorTransform;

    private void Awake()
    {
        StartCoroutine(ChangeCameraBackgroundColor());
        target = cameraStartTarget;
        cameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, target.position, Time.deltaTime * cameraMoveSpeed * 0.5f);
        cameraTransform.rotation = Quaternion.Lerp(cameraTransform.rotation, Quaternion.RotateTowards(cameraTransform.rotation, target.rotation, cameraMoveSpeed), Time.deltaTime * cameraMoveSpeed);
    }

    private IEnumerator ChangeCameraBackgroundColor()
    {
        Color color = Camera.main.backgroundColor;
        while (color.r < 1.0f)
        {
            color.r += Time.deltaTime * 0.5f;
            color.g += Time.deltaTime * 0.5f;
            color.b += Time.deltaTime * 0.5f;
            Camera.main.backgroundColor = color;

            yield return null;
        }

        color.r = color.g = color.b = 1.0f;
        Camera.main.backgroundColor = color;

        FindObjectOfType<TreeQuestion>().StartQuestion();
    }

    public void EndQuestion()
    {
        Camera.main.GetComponent<LookPlayer>().enabled = true;
        target = cameraLastTarget;
    }
}
