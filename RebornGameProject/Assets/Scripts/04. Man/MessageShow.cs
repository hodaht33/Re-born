using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 카카오톡 메시지 표시 
/// </summary>
public class MessageShow : MonoBehaviour
{
    [SerializeField] float period;          // 메시지 표시 주기
    [SerializeField] GameObject[] message;  // 모든 메시지

    private void Start()
    {
        Init();
    }

    // 모든 메시지 숨김
    private void Init()
    {
        foreach (GameObject temp in message)
        {
            temp.SetActive(false);
        }
    }

    // 모든 메시지 순차적으로 표시
    private IEnumerator ShowMessage()
    {
        // 메시지 순차적으로 표시
        for(int i = 0; i < message.Length; i++)
        {
            yield return new WaitForSeconds(period);
            message[i].SetActive(true);
        }
    }
}