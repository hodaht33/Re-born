using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public class StartTreeQuestion : MonoBehaviour
{
    [SerializeField] private Transform playerStartPos;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            other.transform.position = playerStartPos.position;
            other.transform.Rotate(Vector3.up * 90.0f);
            other.GetComponent<PlayerMove>().enabled = false;
            Camera.main.GetComponent<TreeQuestionCamera>().enabled = true;
            //TODO: 카메라 이동, Level2로 컬링마스크 변경 및 나무 문제 시작
        }
    }
}
