#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D mDefaultCursor;
    [SerializeField]
    private Texture2D mInteractCursor;
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
    private int mLayerMask;
    RaycastHit hit;

    public void ControllSandGlassAnim()
    {
        if (mbIsStartSandGlassAnim == true)
        {
            mbIsStartSandGlassAnim = false;
            StopCoroutine(sandGlassCoroutine);
            Cursor.SetCursor(mDefaultCursor, new Vector2(5.0f, 5.0f), CursorMode.ForceSoftware);
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
        // hotspot매개변수에 Vector2 위치 넘겨주어 마우스 화살표 끝에 맞춰 클릭되도록 조정
        Cursor.SetCursor(mDefaultCursor, new Vector2(5.0f, 5.0f), CursorMode.ForceSoftware);

        waitForSeconds = new WaitForSeconds(sandDropTime);
        waitForSecondsForRotate = new WaitForSeconds(rotateGlassTime);
        mLayerMask = 1 << LayerMask.NameToLayer("Tree")
                | 1 << LayerMask.NameToLayer("Interact");
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000.0f, mLayerMask) == true)
        {
            Cursor.SetCursor(mInteractCursor, new Vector2(5.0f, 5.0f), CursorMode.ForceSoftware);
        }
        else
        {
            Cursor.SetCursor(mDefaultCursor, new Vector2(5.0f, 5.0f), CursorMode.ForceSoftware);
        }
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
