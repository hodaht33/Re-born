using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 대화창 시작 및 내용 관리
/// </summary>
public class StartChat : MonoBehaviour
{
    [SerializeField]
    private string[] texts;
    private int currentClickCount = 0;

    // EventTrigger사용할 경우 사용
    public void Click()
    {
        if (Chat.Instance.IsActivateChat == false)
        {
            if (currentClickCount < texts.Length)
            {
                Chat.Instance.ActivateChat(texts[currentClickCount]);
                ++currentClickCount;
            }
            else
            {
                Chat.Instance.ActivateChat(texts[currentClickCount - 1]);
            }
        }
    }

    // Collider를 넣어 사용 할 수 있는 3D오브젝트가 사용
    public void OnMouseDown()
    {
        if (Chat.Instance.IsActivateChat == false)
        {
            if (currentClickCount < texts.Length)
            {
                Chat.Instance.ActivateChat(texts[currentClickCount]);
                ++currentClickCount;
            }
            else
            {
                Chat.Instance.ActivateChat(texts[currentClickCount - 1]);
            }
        }
    }
}
