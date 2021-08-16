using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 메시지 관리
/// </summary>
public class Message : MonoBehaviour
{
    [SerializeField] float period;      // 메시지 표시 주기
    [SerializeField] ScrollRect scroll; // 채팅창 스크롤
    private List<GameObject> message;   // 모든 메시지

    // 메시지 리스트 생성
    public void MakeMessage()
    { 
        message = new List<GameObject>();
        for(int i = 0; i < transform.childCount; i++)
            message.Add(transform.GetChild(i).gameObject);
    }

    // 모든 메시지 순차적으로 표시
    private IEnumerator Show()
    {
        // 메시지 순차적으로 표시
        for(int i = 0; i < message.Count; i++)
        {
            if (message[i].activeInHierarchy)
                continue;

            yield return new WaitForSeconds(period);
            message[i].SetActive(true);
            UpdateScroll();
        }
    }

    // 채팅창 스크롤 최신으로 업데이트
    private void UpdateScroll()
    {
        Canvas.ForceUpdateCanvases();
        scroll.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    // 메시지 표시
    public void ShowMessage()
    {
        StartCoroutine(Show());
    }

    // 메시지 표시 중지
    public void StopShow()
    {
        StopAllCoroutines();
    }
}