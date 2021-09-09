using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 미러방 안에서의 자물쇠, 정답 : 1249
/// </summary>
public class MirrorLock : MonoBehaviour
{
    // 자물쇠 버튼 목록
    [SerializeField] MirrorLockButton[] buttonList;
    
    private Animator open;

    private void Awake()
    {
        open = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        CheckAnswer();
    }

    // 자물쇠 비밀번호 확인
    public void CheckAnswer()
    {
        if (!buttonList[0].clicked) return;
        if (!buttonList[1].clicked) return;
        if (buttonList[2].clicked) return;
        if (!buttonList[3].clicked) return;
        if (buttonList[4].clicked) return;
        if (buttonList[5].clicked) return;
        if (buttonList[6].clicked) return;
        if (buttonList[7].clicked) return;
        if (!buttonList[8].clicked) return;
        if (buttonList[9].clicked) return;

        // 정답이 맞으면 자물쇠 오픈
        open.SetTrigger("open");
    }
}