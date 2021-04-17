using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 단서 관련 컨텐츠를 표시하는 스크립트
/// </summary>
public class ShowContent : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    [SerializeField]
    private float showTime;

    private void OnMouseDown()
    {
        Content.Instance.ShowContent(content, showTime);
    }
}