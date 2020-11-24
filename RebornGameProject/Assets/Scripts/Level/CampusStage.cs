using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public class CampusStage : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform playerStartTransform;
    private Vector3 playerPos;
    
    private void Awake()
    {
        //playerPos = transform.TransformPoint(playerStartTransform.position);
        StageManager.Instance.clearSubway += StartCampus;
    }

    private void StartCampus()
    {
        playerPos = playerStartTransform.position;
        playerTransform.position = playerPos;
    }

}
