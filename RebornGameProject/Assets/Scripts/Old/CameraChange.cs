using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 남자방에서 거울을 통한 이동 담당
/// </summary>
public class CameraChange : MonoBehaviour
{
    // 플레이어와 카메라의 이동 목표
    [SerializeField] Transform playerTarget;
    [SerializeField] Transform cameraTarget;

    // 카메라 매니저 활성 및 비활성
    [SerializeField] CameraManager deactiveManager;
    [SerializeField] CameraManager activeManager;

    // 메인 카메라 및 플레이어
    [SerializeField] Transform main;
    [SerializeField] Transform player;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            player.transform.position = playerTarget.transform.position;
            main.transform.position = cameraTarget.transform.position;

            deactiveManager.active = false;
            activeManager.active = true;
        }
    }
}