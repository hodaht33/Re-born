using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintMessage : MonoBehaviour
{
    public Text chatText;
    public string message;

    private void OnMouseDown()
    {
        //GameObject canvas = GameObject.Find("Canvas");
        //GameObject chat = canvas.transform.FindChild("Chat").gameObject;
        GameObject chat = GameObject.Find("Canvas").transform.FindChild("Chat").gameObject;
        chat.SetActive(true);
        chatText.text = message;
    }
}
