using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 자물쇠의 각 버튼
/// </summary>
public class MirrorLockButton : MonoBehaviour
{
    // 버튼 클릭 전후 Z 위치
    [SerializeField] float before;
    [SerializeField] float after;

    // 클릭 여부
    [HideInInspector] 
    public bool clicked;

    private void Awake()
    {
        clicked = false;
        CheckClicked();
    }

    private void OnMouseDown()
    {
        clicked = !clicked;
        CheckClicked();
    }

    // 클릭 여부에 따라 자물쇠 위치 조정
    private void CheckClicked()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, clicked ? after : before);
    }
}