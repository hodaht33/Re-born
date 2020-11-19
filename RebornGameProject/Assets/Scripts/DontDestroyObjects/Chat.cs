using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 대화창 관리
/// </summary>
public class Chat : SingletonBase<Chat>
{
    [SerializeField] private Canvas chatCanvas;
    [SerializeField] private Text chatText;
    [SerializeField] private float activateTime = 10.0f;

    private Sprite chatImage;
    private EndChat endChat;
    private Image popUpImage;
    private Image chatPanelImage;
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

        chatPanelImage = transform.Find("ChatPanel").GetComponent<Image>();
        popUpImage = transform.Find("ImagePanel").Find("Image").GetComponent<Image>();
        popUpImage.sprite = null;
    }

    public void ActivateChat(string text, Sprite image)
    {
        tickCoroutine = StartCoroutine(TickActivateTime());

        isActivateChat = true;
        chatCanvas.enabled = true;
        chatText.text = text;
        chatPanelImage.enabled = true;

        popUpImage.enabled = true;
        popUpImage.sprite = image;

        endChat.enabled = true;
    }

    public void ActivateChat(string text)
    {
        tickCoroutine = StartCoroutine(TickActivateTime());

        isActivateChat = true;
        chatCanvas.enabled = true;
        chatText.text = text;
        chatPanelImage.enabled = true;

        popUpImage.enabled = false;
        endChat.enabled = true;
    }

    public void ActivateChat(Sprite image)
    {
        tickCoroutine = StartCoroutine(TickActivateTime());

        isActivateChat = true;
        chatCanvas.enabled = true;
        chatPanelImage.enabled = false;
        chatText.text = "";

        popUpImage.enabled = true;
        popUpImage.sprite = image;

        endChat.enabled = true;
    }

    public void DeactivateChat()
    {
        StopCoroutine(tickCoroutine);
        tickCoroutine = null;

        isActivateChat = false;
        chatText.text = "";
        chatPanelImage.enabled = false;

        popUpImage.sprite = null;
        popUpImage.enabled = false;
        chatCanvas.enabled = false;

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