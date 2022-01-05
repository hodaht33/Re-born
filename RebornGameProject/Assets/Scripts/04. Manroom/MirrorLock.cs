using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 미러방 안에서의 자물쇠, 정답 : 1249
/// </summary>
public class MirrorLock : MonoBehaviour
{
    [SerializeField] MirrorLockButton[] buttonList;     // 자물쇠 버튼 목록
    [SerializeField] Sprite letter;                     // 편지 이미지
    [SerializeField] ItemLSH item;                      // 편지 아이템
    [SerializeField] GameObject drawer;                 // 서랍 오브젝트
    [SerializeField] GameObject[] close;                // 자물쇠가 풀리고 종료할 오브젝트

    private Animator open;  // 자물쇠 애니메이터
    private bool active;    // 자물쇠 활성화 여부

    private void Awake()
    {
        open = GetComponent<Animator>();
        active = true;
    }

    private void OnMouseDown()
    {
        CheckAnswer();
    }

    // 자물쇠 비밀번호 확인
    public void CheckAnswer()
    {
        if (!active) return;

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

        active = false;
        StartCoroutine(OpenLetter());
    }

    // 자물쇠 열리고 편지 획득
    private IEnumerator OpenLetter()
    {
        open.SetTrigger("open");
        yield return new WaitForSeconds(3.0f);

        Chat.Instance.ActivateChat("TEXT", letter, true);
        item.OnMouseDown();
        drawer.SetActive(false);

        foreach (GameObject temp in close)
            temp.SetActive(false);
    }
}