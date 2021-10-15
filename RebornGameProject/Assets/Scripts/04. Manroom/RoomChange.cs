using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 미러룸으로의 이동 및 복귀
/// </summary>
public class RoomChange : MonoBehaviour
{
    // 미러룸으로 이동하는지 확인
    [SerializeField] bool mirror;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            SwitchManager.Instance.SetMirror(mirror);
            SwitchManager.Instance.SetPlayer();
            SwitchManager.Instance.CameraView();
        }
    }
}
