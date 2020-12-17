using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 대화창 시작 및 내용 관리
/// </summary>
public class StartChat : MonoBehaviour
{
    [System.Serializable]
    private struct mData
    {
        [SerializeField]
        public Sprite sprite;
        [SerializeField]
        public string text;
        [SerializeField]
        public string sfxName;
        [SerializeField]
        public bool bTime;
    }

    [SerializeField]
    private mData[] mDatas;
    [SerializeField]
    public ItemLSH item;

    private int mCurrentClickCount = 0;

    // EventTrigger사용할 경우 사용
    public void Click()
    {
        if (item != null)
        {
            Chat.Instance.Item = item.ItemName;
        }

        Chat.Instance.StartChat = gameObject;
        Chat.Instance.ActivateChat(mDatas[mCurrentClickCount].text, mDatas[mCurrentClickCount].sprite, mDatas[mCurrentClickCount].bTime);
        EndChat end = Chat.Instance.GetComponent<EndChat>();
        if (end != null)
        {
            end.enabled = true;
        }

        SoundManager.Instance.SetAndPlaySFX(mDatas[mCurrentClickCount].sfxName);
        
        if (mCurrentClickCount < mDatas.Length - 1)
        {
            ++mCurrentClickCount;
        }
    }

    // Collider를 넣어 사용 할 수 있는 3D오브젝트가 사용
    public void OnMouseDown()
    {
        if (item != null)
        {
            Chat.Instance.Item = item.ItemName;
        }

        Chat.Instance.StartChat = gameObject;
        Chat.Instance.ActivateChat(mDatas[mCurrentClickCount].text, mDatas[mCurrentClickCount].sprite, mDatas[mCurrentClickCount].bTime);
        EndChat end = Chat.Instance.GetComponent<EndChat>();
        if (end != null)
        {
            end.enabled = true;
        }

        SoundManager.Instance.SetAndPlaySFX(mDatas[mCurrentClickCount].sfxName);

        if (mCurrentClickCount < mDatas.Length - 1)
        {
            ++mCurrentClickCount;
        }
    }

    public void ChangeSprite(Sprite sprite, int index)
    {
        mDatas[index].sprite = sprite;
    }

    //public void SetLargeImgs(Sprite[] LargeImgs, bool click = false)
    //{
    //    LargeImgs = LargeImgs;

    //    if (click)
    //    {
    //        Click();
    //    }
    //}

    //public Sprite[] getItemImg() { return itemImg; }
}