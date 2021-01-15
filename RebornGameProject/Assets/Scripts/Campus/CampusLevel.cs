#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampusLevel : LevelPuzzle
{
    private Transform mTarget;
    [SerializeField]
    private Transform mCameraStartTarget;
    [SerializeField]
    private Transform mCameraSecondTarget;
    [SerializeField]
    private Transform mCameraLastTarget;
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
