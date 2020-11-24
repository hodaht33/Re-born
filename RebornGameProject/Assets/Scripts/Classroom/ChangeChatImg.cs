using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChatImg : MonoBehaviour
{
    [SerializeField]
    private StartChat startChat;
    [SerializeField]
    private Sprite sprite;
    [SerializeField]
    private int index;

    private void OnMouseDown()
    {
        if (startChat != null)
        {
            startChat.ChangeSprite(sprite, 0);
        }
    }
}
