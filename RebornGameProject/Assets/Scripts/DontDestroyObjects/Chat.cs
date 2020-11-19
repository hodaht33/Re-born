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
    [SerializeField]
    private float activateTime = 10.0f;

    private Sprite chatImage;
    private EndChat endChat;
    private Image popUpPanelImage;
    private Image popUpImage;
    private Image chatPanelImage;
    private Coroutine tickCoroutine = null;

    private bool isActivateChat = false;
    public bool IsActivateChat
    {
        get { return isActivateChat; }
        private set { }
    }
    private string item = null;
    public string Item
    {
        get { return item; }
        set { item = value; }
    }
    private GameObject startChat = null;
    public GameObject StartChat
    {
        get { return startChat; }
        set { startChat = value; }
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
        popUpPanelImage = transform.Find("ImagePanel").GetComponent<Image>();
        popUpImage = transform.Find("ImagePanel").transform.Find("Image").GetComponent<Image>();
        popUpImage.sprite = null;
    }

    public void ActivateChat(string text, Sprite image, bool time = true)
    {
        if (time)
        {
            tickCoroutine = StartCoroutine(TickActivateTime());
        }

        isActivateChat = true;
        chatCanvas.enabled = true;
        chatText.text = text;
        chatPanelImage.enabled = true;

        popUpPanelImage.enabled = true;
        popUpImage.enabled = true;
        popUpImage.sprite = image;

        endChat.enabled = true;
    }

    public void ActivateChat(string text, bool time = true)
    {
        if (time)
        {
            tickCoroutine = StartCoroutine(TickActivateTime());
        }

        isActivateChat = true;
        chatCanvas.enabled = true;
        chatText.text = text;
        chatPanelImage.enabled = true;

        popUpPanelImage.enabled = false;
        popUpImage.enabled = false;
        endChat.enabled = true;
    }

    public void ActivateChat(Sprite image, bool time = true)
    {
        if (time)
        {
            tickCoroutine = StartCoroutine(TickActivateTime());
        }

        isActivateChat = true;
        chatCanvas.enabled = true;
        chatPanelImage.enabled = false;
        chatText.text = "";

        popUpPanelImage.enabled = true;
        popUpImage.enabled = true;
        popUpImage.sprite = image;

        endChat.enabled = true;
    }

    public void DeactivateChat()
    {
        if (tickCoroutine != null)
        {
            StopCoroutine(tickCoroutine);
        }
        tickCoroutine = null;

        isActivateChat = false;
        chatText.text = "";
        chatPanelImage.enabled = false;

        popUpImage.sprite = null;
        popUpImage.enabled = false;
        popUpPanelImage.enabled = false;
        chatCanvas.enabled = false;

        endChat.enabled = false;

        if (item != "" && item != null)
        {
            if (Inventory.instance.UseSelectedItem(item))
            {
                Sprite[] img = startChat.GetComponent<StartChat>().getItemImg();
                startChat.GetComponent<StartChat>().SetLargeImgs(img, true);
                return;
            }
        }
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
