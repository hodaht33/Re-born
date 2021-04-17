#pragma warning disable CS0649

using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 선형보간을 이용한 카메라 이동
/// </summary>
public class CamMoveL : MonoBehaviour
{
    [SerializeField]
    private Transform mCameraTransform;

    [SerializeField]
    private float mCameraSpeed = 5.0f;

    [SerializeField]
    private float scrollSpeed;

    // 마우스 축소 범위
    [SerializeField]
    private float minView;

    // 마우스 확대 범위
    [SerializeField]
    private float maxView;

    // 마우스 휠 관련 변수
    private float scroll;
    private Camera lookCamera;

    private void Start()
    {
        lookCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        // 마우스 휠 확대, 축소 구현
        scroll = Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;

        if (lookCamera.fieldOfView - scroll < minView)
            lookCamera.fieldOfView = minView;
        else if (lookCamera.fieldOfView - scroll > maxView)
            lookCamera.fieldOfView = maxView;
        else
            lookCamera.fieldOfView -= scroll;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, mCameraTransform.position, Time.deltaTime * mCameraSpeed);
    }
}
