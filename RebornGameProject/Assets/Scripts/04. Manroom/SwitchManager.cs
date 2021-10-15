using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 미러룸으로의 이동 및 복귀에 따른 카메라, 플레이어 조정
/// </summary>
public class SwitchManager : SingletonBase<SwitchManager>
{
    // 각 방의 카메라 매니저
    [SerializeField] CameraSwitch normalSwitch;
    [SerializeField] CameraSwitch mirrorSwitch;

    // 각 방에서 플레이어 시작 위치
    [SerializeField] Transform normalTransform;
    [SerializeField] Transform mirrorTransform;

    // 플레이어 위치
    [SerializeField] Transform player;

    // 현재 시점 및 미러룸 
    private View current;
    private bool mirror;

    // 시작 카메라 설정
    private void Start()
    {
        current = View.center;
        mirror = false;
    }

    public void LeftView()
    {
        current = View.left;
        CameraView();
    }

    public void CenterView()
    {
        current = View.center;
        CameraView();
    }

    public void RightView()
    {
        current = View.right;
        CameraView();
    }

    // 미러룸인지 설정
    public void SetMirror(bool flag)
    {
        mirror = flag;
    }

    // 플레이어의 미러룸 이동 및 복귀
    public void SetPlayer()
    {
        player.transform.position = mirror ? mirrorTransform.position : normalTransform.position;
    }

    // 현재 시점에 따른 카메라 뷰 설정
    public void CameraView()
    {
        if (!mirror)
            normalSwitch.SwithcView(current);
        else
            mirrorSwitch.SwithcView(current);
    }
}