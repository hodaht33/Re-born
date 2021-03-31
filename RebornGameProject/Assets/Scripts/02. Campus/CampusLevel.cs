#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 캠퍼스 레벨 관리
/// </summary>
public class CampusLevel : LevelPuzzle
{
    private Transform mTarget;  // 카메라의 위치
    [SerializeField]
    private Transform mCameraStartTarget;   // 카메라 시작 위치
    [SerializeField]
    private Transform mCameraLastTarget;    // 퍼즐 종료 후 카메라 위치
    private Transform mCameraTransform;

    [SerializeField]
    private float mCameraMoveSpeed = 5.0f;

    [SerializeField]
    private TreeQuestion mTreeQuestion;

    public override void EndLevel()
    {
        Camera.main.GetComponent<LookPlayer>().enabled = true;
        mTarget = mCameraLastTarget;
    }

    private new void Awake()
    {
        base.Awake();
        mData.mNextSceneTrigger.OnEndLevel += EndLevel;
        StartCoroutine(ChangeCameraBackgroundColorCoroutine());
        mTarget = mCameraStartTarget;
        mCameraTransform = Camera.main.transform;
    }

    private void LateUpdate()
    {
        mCameraTransform.position = Vector3.Lerp(mCameraTransform.position, mTarget.position, Time.deltaTime * mCameraMoveSpeed * 0.5f);
        mCameraTransform.rotation = Quaternion.Lerp(mCameraTransform.rotation, Quaternion.RotateTowards(mCameraTransform.rotation, mTarget.rotation, mCameraMoveSpeed), Time.deltaTime * mCameraMoveSpeed);
    }

    // 배경색이 검은색에서 점차 흰색으로 변하게하는 코루틴
    private IEnumerator ChangeCameraBackgroundColorCoroutine()
    {
        Color color = Camera.main.backgroundColor;
        while (color.r < 1.0f)
        {
            color.r += Time.deltaTime * 0.5f;
            color.g += Time.deltaTime * 0.5f;
            color.b += Time.deltaTime * 0.5f;
            Camera.main.backgroundColor = color;

            yield return null;
        }

        color.r = color.g = color.b = 1.0f;
        Camera.main.backgroundColor = color;

        mTreeQuestion.StartPuzzle();
    }


}
