using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D mDefaultCursor;
    private List<Texture2D> mSandGlassAnimList;
    private int mIndex = 0;
    private Vector2 mCursorSize;
    private bool mIsStartSandGlassAnim;

    private void Awake()
    {

        // 이미지 타입을 Cursor로 하지 않으면 invalid texture used for cursor 경고 발생
        Cursor.SetCursor(mDefaultCursor, Vector2.zero, CursorMode.ForceSoftware);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) == true)
        {
            mIsStartSandGlassAnim = mIsStartSandGlassAnim == true ? false : true;

        }
    }

    private void ControllSandGlass()
    {

    }

    private IEnumerator StartSandGlassAnim()
    {


        yield return null;
    }
}
