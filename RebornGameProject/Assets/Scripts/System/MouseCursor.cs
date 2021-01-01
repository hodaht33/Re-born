#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D mDefaultCursor;
    [SerializeField]
    private List<Texture2D> mSandGlassAnimList;
    private Vector2 mCursorSize;
    private bool mbIsStartSandGlassAnim;
    private Coroutine sandGlassCoroutine;
    [SerializeField]
    private float sandDropTime = 0.2f;
    [SerializeField]
    private float rotateGlassTime = 0.05f;
    private WaitForSeconds waitForSeconds;
    private WaitForSeconds waitForSecondsForRotate;

    public void ControllSandGlassAnim()
    {
        if (mbIsStartSandGlassAnim == true)
        {
            mbIsStartSandGlassAnim = false;
            StopCoroutine(sandGlassCoroutine);
            Cursor.SetCursor(mDefaultCursor, Vector2.zero, CursorMode.ForceSoftware);
        }
        else
        {
            mbIsStartSandGlassAnim = true;
            sandGlassCoroutine = StartCoroutine(StartSandGlassAnim());
        }
    }

    private void Awake()
    {
        // 이미지 타입을 Cursor로 하지 않으면 invalid texture used for cursor 경고 발생
        Cursor.SetCursor(mDefaultCursor, Vector2.zero, CursorMode.ForceSoftware);

        waitForSeconds = new WaitForSeconds(sandDropTime);
        waitForSecondsForRotate = new WaitForSeconds(rotateGlassTime);
    }

    private IEnumerator StartSandGlassAnim()
    {
        int index = -1;

        while (true)
        {
            index = (index + 1) % mSandGlassAnimList.Count;
            Cursor.SetCursor(mSandGlassAnimList[index], Vector2.zero, CursorMode.ForceSoftware);

            if (index < 3)
            {
                yield return waitForSeconds;
            }
            else
            {
                yield return waitForSecondsForRotate;
            }
        }
    }
}
