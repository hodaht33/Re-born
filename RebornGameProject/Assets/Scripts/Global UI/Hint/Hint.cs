using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 힌트 오브젝트 관련 기능
/// </summary>
public class Hint : MonoBehaviour
{
    private void Start()
    {
        MousePoint.Instance.pointObject += ClickHint;
    }

    // 힌트 오브젝트를 클릭했을 경우
    private void ClickHint(Transform obj)
    {
        if(obj == transform)
        {
            if (!HintManager.Instance.CheckHintGage())
                return;

            MousePoint.Instance.pointObject -= ClickHint;
            HintManager.Instance.StartMoveIcon(MousePoint.Instance.mainCamera.WorldToScreenPoint(obj.position));
        }
    }
}