using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 자물쇠 정답일 경우 자물쇠 그림 끄기! 아마도 삭제!
/// </summary>
#region Puzzle클래스 상속(이성호)
#endregion
public class OpenLock : Puzzle
{
    private void OnMouseDown()
    {
        GameObject lockObject = GameObject.Find("Canvas").transform.Find("lock").gameObject;

        if (lockObject.activeSelf == false)
        {
            lockObject.SetActive(true);
            GameObject.Find("Canvas").transform.Find("back_gray").gameObject.SetActive(true);
        }
    }

    public override void StartPuzzle()
    {

    }

    public override void EndPuzzle()
    {

    }
}
