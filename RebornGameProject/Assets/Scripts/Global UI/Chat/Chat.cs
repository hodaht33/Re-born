#pragma warning disable CS0649

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ۼ��� : �̼�ȣ
/// ��� : ��ȭâ ����
/// </summary>
public class Chat : SingletonBase<Chat>
{
    [SerializeField]
    private Text mChatText;
    [SerializeField]
    private float mActivateTime = 10.0f;

    private Canvas mChatCanvas; // ��ȭâ ĵ����
    private EndChat mEndChat;   // ��ȭâ �����Ű�� ��ü
    private Image mPopUpPanelImage; // �˾�â �г�
    private Image mPopUpImage;  // �˾�â ���� �̹���
    private Image mChatPanelImage;  // ��ȭâ �г�
    private Coroutine mTickCoroutine = null;
    private Coroutine mDefaultTickCoroutine = null;
    private bool mbEndDefaultTickTime = true;   // ��ȭâ ���� ����

    private bool mbIsActivateChat = false;  // ��ȭâ Ȱ��ȭ ����
    public bool IsActivateChat
    {
        get
        {
            return mbIsActivateChat;
        }
        private set
        {

        }
    }

    private string mItem = null;    // ��� ������
    public string Item
    {
        get
        {
            return mItem;
        }
        set
        {
            mItem = value;
        }
    }

    private GameObject mStartChat = null;   // ��ȭâ ������Ʈ�� �ܺο��� �����ϱ� ����
    public GameObject StartChat
    {
        get
        {
            return mStartChat;
        }
        set
        {
            mStartChat = value;
        }
    }

    // ��ȭâ Ȱ��ȭ �޼���
    public void ActivateChat(string text, Sprite spriteOrNull, bool time)
    {
        mbEndDefaultTickTime = false;

        if (mDefaultTickCoroutine != null)
        {
            StopCoroutine(mDefaultTickCoroutine);
        }
        mDefaultTickCoroutine = StartCoroutine(TickDefaultActiveTimeCoroutine());

        if (time == true)
        {
            if (mTickCoroutine != null)
            {
                StopCoroutine(mTickCoroutine);
            }

            mTickCoroutine = StartCoroutine(TickActivateTimeCoroutine());
        }

        mChatCanvas.enabled = true;

        if (text.Trim() != ""
            && spriteOrNull != null)    // ������ ������ �̹����� ���� ���
        {
            mChatText.text = text;
            mChatPanelImage.enabled = true;

            mPopUpPanelImage.enabled = true;
            mPopUpImage.enabled = true;
            mPopUpImage.sprite = spriteOrNull;
        }
        else if (text.Trim() != "") // ���븸 ���� ���
        {
            mChatText.text = text;
            mChatPanelImage.enabled = true;
        }
        else if (spriteOrNull != null)  // �̹����� ���� ���
        {
            mPopUpPanelImage.enabled = true;
            mPopUpImage.enabled = true;
            mPopUpImage.sprite = spriteOrNull;
        }
        else
        {
            DeactivateChat();
            //mChatCanvas.enabled = false;
        }
    }

    // ��ȭâ ��Ȱ��ȭ �޼���
    public void DeactivateChat()
    {
        if (mbEndDefaultTickTime == false)
        {
            return;
        }

        if (mTickCoroutine != null)
        {
            StopCoroutine(mTickCoroutine);
        }
        mTickCoroutine = null;

        mChatText.text = "";
        mChatPanelImage.enabled = false;

        mPopUpImage.sprite = null;
        mPopUpImage.enabled = false;
        mPopUpPanelImage.enabled = false;

        mChatCanvas.enabled = false;

        mEndChat.enabled = false;

        //if (item != "" && item != null)
        //{
        //    if (Inventory.instance.UseSelectedItem(item))
        //    {
        //        Sprite[] img = startChat.GetComponent<StartChat>().getItemImg();
        //        startChat.GetComponent<StartChat>().SetLargeImgs(img, true);
        //        return;
        //    }
        //}
    }

    protected override void Awake()
    {
        base.Awake();

        mChatCanvas = GetComponent<Canvas>();

        mEndChat = GetComponent<EndChat>();
        mEndChat.endChatEvent += DeactivateChat;

        mChatPanelImage = transform.Find("ChatPanel").GetComponent<Image>();

        mPopUpPanelImage = transform.Find("ImagePanel").GetComponent<Image>();
        mPopUpImage = transform.Find("ImagePanel").transform.Find("Image").GetComponent<Image>();
        mPopUpImage.sprite = null;

        DeactivateChat();
    }

    private IEnumerator TickActivateTimeCoroutine()
    {
        float tickTime = 0.0f;

        while (tickTime <= mActivateTime)
        {
            tickTime = tickTime + Time.deltaTime;

            yield return null;
        }

        DeactivateChat();
    }

    private IEnumerator TickDefaultActiveTimeCoroutine()
    {
        float tickTime = 0.0f;

        while (tickTime <= 0.2f)
        {
            tickTime += Time.deltaTime;

            yield return null;
        }

        mbEndDefaultTickTime = true;
    }
}
