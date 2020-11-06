using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 대화창 시작 및 내용 관리
/// </summary>
public class StartChat : MonoBehaviour
{
    [SerializeField]
    private string[] texts;
    private int currentClickCount = 0;
    private int currentClickCountImg = 0;
    public Sprite[] LargeImgs;
    public int state = 0; // 0: chat + image, 1: image, 2: chat

    // EventTrigger사용할 경우 사용
    public void Click()
    {

        if (Chat.Instance.IsActivateChat == false & state != 1)
        {
            if (currentClickCount >= texts.Length)
                currentClickCount = texts.Length - 1;
            if (currentClickCountImg >= LargeImgs.Length)
                currentClickCountImg = LargeImgs.Length - 1;

            Chat.Instance.ActivateChat(texts[currentClickCount], LargeImgs[currentClickCount]);
            ++currentClickCount;
            ++currentClickCountImg;
        }
    }

    // Collider를 넣어 사용 할 수 있는 3D오브젝트가 사용
    public void OnMouseDown()
    {
        if (Chat.Instance.IsActivateChat == false & state != 1)
        {
            if (currentClickCount >= texts.Length)
                currentClickCount = texts.Length - 1;
            if (currentClickCountImg >= LargeImgs.Length)
                currentClickCountImg = LargeImgs.Length - 1;

            Chat.Instance.ActivateChat(texts[currentClickCount], LargeImgs[currentClickCount]);
            ++currentClickCount;
            ++currentClickCountImg;
        }
    }
}
