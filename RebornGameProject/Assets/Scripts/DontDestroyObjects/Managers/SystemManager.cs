using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 게임 시스템 관리
/// </summary>
public class SystemManager : MonoBehaviour
{
    private MouseCursor mouseCursor;

    private void Awake()
    {
        mouseCursor = transform.Find("MouseCursor").GetComponent<MouseCursor>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N) == true)
        {
            Loading();
        }
    }

    public void Loading()
    {
        mouseCursor.ControllSandGlassAnim();
    }
}
