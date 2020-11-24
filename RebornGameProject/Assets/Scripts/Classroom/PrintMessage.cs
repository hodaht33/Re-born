using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 삭제삭제!
/// </summary>

public class PrintMessage : MonoBehaviour
{
    [SerializeField]
    private Text mChatText;
    [SerializeField]
    private string mMessage;

    private void OnMouseDown()
    {
        //GameObject canvas = GameObject.Find("Canvas");
        //GameObject chat = canvas.transform.FindChild("Chat").gameObject;
        GameObject chat = GameObject.Find("Canvas").transform.Find("Chat").gameObject;
        chat.SetActive(true);
        mChatText.text = mMessage;
    }
}
