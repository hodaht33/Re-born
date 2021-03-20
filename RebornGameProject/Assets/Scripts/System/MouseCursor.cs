#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 마우스 커서 변경
/// </summary>
public class MouseCursor : MonoBehaviour
{
    [SerializeField]
    private Texture2D mDefaultCursor;   // 기본 마우스 커서 이미지
    [SerializeField]
    private Texture2D mInteractCursor;  // 상호작용 마우스 커서 이미지
    [SerializeField]
    private List<Texture2D> mSandGlassAnimList; // 모래시계 커서 스프라이트 이미지
    private bool mbIsStartSandGlassAnim;    // 모래시계 애니메이션 활성화 여부
    private Coroutine sandGlassCoroutine;   // 모래시계 애니메이션 코루틴
    [SerializeField]
    private float sandDropTime = 0.2f;  // 모래 떨어지는 애니메이션 재생 텀
    [SerializeField]
    private float rotateGlassTime = 0.05f;  // 모래시계 회전 애니메이션 재생 텀
    private WaitForSeconds waitForSeconds;
    private WaitForSeconds waitForSecondsForRotate;
    private int mLayerMask;
    private RaycastHit hit;
    private Ray ray;

    // 모래시계 애니메이션 제어 메서드
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
            sandGlassCoroutine = StartCoroutine(PlaySandGlassAnim());
        }
    }

    private void Awake()
    {
        // 이미지 타입을 Cursor로 하지 않으면 invalid texture used for cursor 경고 발생
        // hotspot매개변수에 Vector2 위치 넘겨주어 마우스 화살표 끝에 맞춰 클릭되도록 조정
        Cursor.SetCursor(mDefaultCursor, new Vector2(5.0f, 5.0f), CursorMode.ForceSoftware);

        waitForSeconds = new WaitForSeconds(sandDropTime);
        waitForSecondsForRotate = new WaitForSeconds(rotateGlassTime);

        // Tree와 Interact레이어를 마우스로 가리킬 때
        // 마우스 커서가 mInteractCursor 로 바뀌도록 하기위한 레이어마스크
        mLayerMask = 1 << LayerMask.NameToLayer("Tree")
                | 1 << LayerMask.NameToLayer("Interact");
    }

    private void Update()
    {
        // 모래시계가 재생되고 있지 않을 경우에만
        // 일반 마우스커서와 상호작용 마우스 커서가 동작하도록 함
        if (mbIsStartSandGlassAnim == false)
        {
            // 레이어마스크에 따라 마우스 커서 변경
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
    }

    // 모래시계 애니메이션 재생 코루틴 메서드
    private IEnumerator PlaySandGlassAnim()
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
