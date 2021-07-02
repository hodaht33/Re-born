using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카카오톡 메시지 표시 
/// </summary>
public class Message : MonoBehaviour
{
    [SerializeField] float period;          // 메시지 표시 주기
    [SerializeField] GameObject[] message;  // 모든 메시지

    // 모든 메시지 순차적으로 표시
    private IEnumerator Show()
    {
        // 메시지 순차적으로 표시
        for(int i = 0; i < message.Length; i++)
        {
            if (message[i].activeInHierarchy)
                continue;

            yield return new WaitForSeconds(period);
            message[i].SetActive(true);
        }
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