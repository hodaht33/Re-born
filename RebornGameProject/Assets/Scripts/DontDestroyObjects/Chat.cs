using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 대화창 관리
/// </summary>
public class Chat : SingletonBase<Chat>
{
    [SerializeField]
    private Canvas chatCanvas;
    [SerializeField]
    private Text chatText;
    private EndChat endChat;

    [SerializeField]
    private float activateTime = 10.0f;
    private Coroutine tickCoroutine = null;

    private bool isActivateChat = false;
    public bool IsActivateChat
    {
        get { return isActivateChat; }
        private set { }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        
        endChat = GetComponent<EndChat>();
        endChat.endChatEvent += DeactivateChat;
    }

    public void ActivateChat(string text)
    {
        tickCoroutine = StartCoroutine(TickActivateTime());

        isActivateChat = true;
        chatCanvas.enabled = true;
        chatText.text = text;
        endChat.enabled = true;
    }

    public void DeactivateChat()
    {
        StopCoroutine(tickCoroutine);
        tickCoroutine = null;

        isActivateChat = false;
        chatCanvas.enabled = false;
        chatText.text = "";
        endChat.enabled = false;
    }

    private IEnumerator TickActivateTime()
    {
        float tickTime = 0.0f;

        while (tickTime <= activateTime)
        {
            tickTime = tickTime + Time.deltaTime;

            yield return null;
        }

        DeactivateChat();
    }
}
