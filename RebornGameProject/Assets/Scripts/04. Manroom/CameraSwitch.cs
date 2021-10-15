using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 카메라 구도 변경
/// </summary>
public class CameraSwitch : MonoBehaviour
{
    [SerializeField] Transform main;    // 메인 카메라 트랜스폼

    // 카메라 시점에 따른 트랜스폼
    [SerializeField] Transform leftTransform;
    [SerializeField] Transform centerTransform;
    [SerializeField] Transform rightTransform;

    // 시점 아이콘
    [SerializeField] Image leftImage;
    [SerializeField] Image centerImage;
    [SerializeField] Image rightImage;

    // 시점 아이콘 스프라이트
    [SerializeField] Sprite leftOFF;
    [SerializeField] Sprite leftON;
    [SerializeField] Sprite centerOFF;
    [SerializeField] Sprite centerON;
    [SerializeField] Sprite rightOFF;
    [SerializeField] Sprite rightON;

    // 시점에 따른 이미지 및 카메라 구도 조정
    public void SwithcView(View view)
    {
        AllIconOFF();

        switch (view)
        {
            case View.left:
                main.position = leftTransform.position;
                main.rotation = leftTransform.rotation;
                leftImage.sprite = leftON;
                break;

            case View.center:
                main.position = centerTransform.position;
                main.rotation = centerTransform.rotation;
                centerImage.sprite = centerON;
                break;

            case View.right:
                main.position = rightTransform.position;
                main.rotation = rightTransform.rotation;
                rightImage.sprite = rightON;
                break;

            default:
                break;
        }
    }

    // 모든 아이콘 이미지를 OFF로 바꿈
    private void AllIconOFF()
    {
        leftImage.sprite = leftOFF;
        centerImage.sprite = centerOFF;
        rightImage.sprite = rightOFF;
    }
}

// 시점 종류
public enum View
{
    left,
    center,
    right,
};