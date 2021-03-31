#pragma warning disable CS0649

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
    private Text mChatText;
    [SerializeField]
    private float mActivateTime = 10.0f;

    private Canvas mChatCanvas; // 대화창 캔버스
    private EndChat mEndChat;   // 대화창 종료시키는 객체
    private Image mPopUpPanelImage; // 팝업창 패널
    private Image mPopUpImage;  // 팝업창 내의 이미지
    private Image mChatPanelImage;  // 대화창 패널
    private Coroutine mTickCoroutine = null;
    private Coroutine mDefaultTickCoroutine = null;
    private bool mbEndDefaultTickTime = true;   // 대화창 종료 여부

    private bool mbIsActivateChat = false;  // 대화창 활성화 여부
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

    private string mItem = null;    // 대상 아이템
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

    private GameObject mStartChat = null;   // 대화창 오브젝트를 외부에서 지정하기 위함
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

    // 대화창 활성화 메서드
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
            && spriteOrNull != null)    // 내용이 없으며 이미지가 없는 경우
        {
            mChatText.text = text;
            mChatPanelImage.enabled = true;

            mPopUpPanelImage.enabled = true;
            mPopUpImage.enabled = true;
            mPopUpImage.sprite = spriteOrNull;
        }
        else if (text.Trim() != "") // 내용만 없는 경우
        {
            mChatText.text = text;
            mChatPanelImage.enabled = true;
        }
        else if (spriteOrNull != null)  // 이미지만 없는 경우
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
    
    // 대화창 비활성화 메서드
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

    private void Awake()
    {
        if (instance != null &&
            instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

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
