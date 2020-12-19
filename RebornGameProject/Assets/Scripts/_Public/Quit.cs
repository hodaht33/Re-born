using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 게임 종료
/// </summary>
public class Quit : MonoBehaviour
{
    public void onClick()
    {
        // 에디터인 경우
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
        //}
    }
}
