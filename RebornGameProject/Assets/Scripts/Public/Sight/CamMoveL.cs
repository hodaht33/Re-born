#pragma warning disable CS0649

using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 선형보간을 이용한 카메라 이동
/// 수정자 : 곽진성
/// 기능 : 휠 확대, 축소 기능 추가 및 기존 방식 수정
/// </summary>
public class CamMoveL : MonoBehaviour
{
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

    // 플레이어 트랜스폼과 플레이어와의 거리
    [SerializeField] Transform player;
    private Vector3 distance;

    private void Start()
    {
        lookCamera = GetComponent<Camera>();
        distance = transform.position - player.position;
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
        transform.position = Vector3.Lerp(transform.position, player.position + distance, Time.deltaTime * mCameraSpeed);
    }
}