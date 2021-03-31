#pragma warning disable CS0649

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ۼ��� : �̼�ȣ
/// ��� : ��ȭâ ����
/// </summary>
public class Chat : SingletonBase<Chat>
{
    // UI components
    [SerializeField]
    private Text mChatText;
    private Canvas chatCanvas; // ��ȭâ ĵ����
    private Image chatImagePanel; // �˾�â �г�
    private Image chatImage;  // �˾�â ���� �̹���
    private Image chatTextPanel;  // ��ȭâ �г�

    // Chat default activation time (Unit = Second).
    public float ChatActivationTime = 2.0f;

    // Chat live time (Unit = Second). if chatVisibleTime < 0, chat dissapears.
    private float chatRemainingTime = 0;

    public bool IsChatActivated { get => chatRemainingTime > 0; }

    private void Update()
    {
        if (chatRemainingTime > 0) chatRemainingTime -= Time.deltaTime;
        else if (chatRemainingTime < 0) DeactivateChat();
    }

    public void DeactivateChat()
    {
        Debug.Log("Deactivate chat");

        // Reset chat timer
        chatRemainingTime = 0;

        // Disable this script so that Update() method does not called needlessly
        enabled = false;

        // Hide all chat-related UIs.
        chatCanvas.enabled = false;
        chatTextPanel.enabled = false;
        chatImagePanel.enabled = false;
    }

    public void ActivateChat(string text, Sprite spriteOrNull, bool time)
    {
        // Remove existing chat content.
        DeactivateChat();

        Debug.Log("Activate chat : " + text);

        // Enable chat canvas
        chatCanvas.enabled = false;

        // Show text if text is not empty.
        if (text.Trim() != "")
        {
            mChatText.text = text;
            chatTextPanel.enabled = true;
        }

        // Show sprite if the given sprite is not empty
        if (spriteOrNull)
        {
            chatImagePanel.enabled = true;
            chatImage.sprite = spriteOrNull;
        }

        // Initialize timer
        chatRemainingTime = ChatActivationTime;

        // Start timer(which is implemented by update method) by enabling this script.
        enabled = true;
    }

    protected override void Awake()
    {
        base.Awake();

        chatCanvas = GetComponent<Canvas>();
        chatTextPanel = transform.Find("ChatPanel").GetComponent<Image>();
        chatImagePanel = transform.Find("ImagePanel").GetComponent<Image>();
        chatImage = transform.Find("ImagePanel").transform.Find("Image").GetComponent<Image>();
        DeactivateChat();
    }
}
