using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 게임 종료
/// </summary>
public class Quit : MonoBehaviour
{
    private float mTimer;
    int waitingTime;

    void Start()
    {
        mTimer = 0.0f;
        waitingTime = 1;
        //inside = false;
    }

    void Update()
    {
        mTimer += Time.deltaTime;
    }

    public void onClick()
    {
        //if (timer > waitingTime)
        //{

        // 에디터인 경우
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
        //}
    }
}
