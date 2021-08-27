using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 두 기점을 사이로 플레이어 움직임에 기반하여 카메라 위치 및 각도 조정
/// </summary>
public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform playerStart; // 플레이어 시작 트랜스폼
    [SerializeField] Transform playerEnd;   // 플레이어 끝 트랜스폼

    [SerializeField] Transform cameraStart; // 카메라 시작 트랜스폼
    [SerializeField] Transform cameraEnd;   // 카메라 끝 트랜스폼

    [SerializeField] Transform player;      // 실제 플레이어 트랜스폼
    [SerializeField] Transform mainCamera;  // 실제 카메라 트랜스폼

    [SerializeField] Vector3 targetPosition;
    [SerializeField] Quaternion targetRotation;

    // 플레이어의 위치에 따른 비율
    private float playerRatio;
    private float playerStartDistance;
    private float playerEndDistance;

    // 카메라 매니저 담당
    public bool active;

    private void LateUpdate()
    {
        if (!active) return;

        playerStartDistance = Vector3.Distance(playerStart.position, player.position);
        playerEndDistance = Vector3.Distance(player.position, playerEnd.position);
        playerRatio = playerStartDistance / (playerStartDistance + playerEndDistance);

        targetPosition = Vector3.Lerp(cameraStart.position, cameraEnd.position, playerRatio);
        targetRotation = Quaternion.Lerp(cameraStart.rotation, cameraEnd.rotation, playerRatio);

        mainCamera.position = Vector3.Lerp(mainCamera.position, targetPosition, Time.deltaTime);
        mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, targetRotation, Time.deltaTime);
    }
}