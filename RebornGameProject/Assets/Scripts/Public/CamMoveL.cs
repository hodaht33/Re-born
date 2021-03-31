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

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, mCameraTransform.position, Time.deltaTime * mCameraSpeed);
    }
}
