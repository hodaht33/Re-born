#pragma warning disable CS0649

using UnityEngine;
using UnityEngine.UI;

public class Chat : SingletonBase<Chat>
{
    // UI components
    [SerializeField]
    private Canvas chatCanvas;          // Chat canvas that contains all below UIs
    private GameObject chatTextPanel;   // Panel that contains chat text
    private Text chatText;              // Chat text
    private GameObject chatImagePanel;  // Panel that contains popup image
    private Image chatImage;            // Popup image

    // Chat default activation time (Unit = Second).
    public float ChatActivationTime = 2.0f;

    // Chat live time (Unit = Second). if chatVisibleTime < 0, chat dissapears.
    private float chatRemainingTime = 0;

    public bool IsChatActivated { get => chatRemainingTime > 0; }

    private void Update()
    {
        // It user press the enter key or the space key, deactivate chat.
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Space)) chatRemainingTime = -1;

        if (chatRemainingTime > 0) chatRemainingTime -= Time.deltaTime;
        else if (chatRemainingTime < 0) DeactivateChat();
    }

    public void DeactivateChat()
    {
        // Reset chat timer
        chatRemainingTime = 0;

        // Disable this script so that Update() method does not called needlessly
        enabled = false;

        // Hide all chat-related UIs.
        chatCanvas.enabled = false;
        chatTextPanel.SetActive(false);
        chatImagePanel.SetActive(false);
    }

    public void ActivateChat(string text, Sprite sprite, bool time)
    {
        // Remove existing chat content.
        DeactivateChat();

        // Enable chat canvas
        chatCanvas.enabled = true;

        // Show text if text is not empty.
        if (text.Trim() != "")
        {
            chatTextPanel.SetActive(true);
            chatText.text = text;
        }

        // Show sprite if the given sprite is not empty
        if (sprite)
        {
            chatImagePanel.SetActive(true);
            chatImage.sprite = sprite;
        }

        // Initialize timer
        chatRemainingTime = ChatActivationTime;

        // Start timer(which is implemented by update method) by enabling this script.
        enabled = true;
    }

    public void ActivateImage(GameObject gameObject)
    {

    }

    protected override void Awake()
    {
        base.Awake();
        chatCanvas = GetComponent<Canvas>();
        chatTextPanel = transform.Find("ChatPanel").gameObject;
        chatText = transform.Find("ChatPanel/Text").GetComponent<Text>();
        chatImagePanel = transform.Find("ImagePanel").gameObject;
        chatImage = transform.Find("ImagePanel/Image").GetComponent<Image>();
        DeactivateChat();
    }
}
