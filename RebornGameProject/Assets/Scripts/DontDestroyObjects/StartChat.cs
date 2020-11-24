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
    private struct data
    {
        [SerializeField]
        public string text;
        [SerializeField]
        public Sprite sprite;
        [SerializeField]
        public string sfxName;
        [SerializeField]
        public bool bTime;
    }

    [SerializeField]
    private data[] datas;
    [SerializeField]
    public ItemLSH item;

    private int currentClickCount;

    // EventTrigger사용할 경우 사용
    public void Click()
    {
        if (item != null)
        {
            Chat.Instance.Item = item.ItemName;
        }

        Chat.Instance.StartChat = gameObject;
        Chat.Instance.ActivateChat(datas[currentClickCount].text, datas[currentClickCount].sprite, datas[currentClickCount].bTime);

        SoundManager.Instance.SetAndPlaySFX(datas[currentClickCount].sfxName);
        
        if (currentClickCount < datas.Length - 1)
        {
            ++currentClickCount;
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
        Chat.Instance.ActivateChat(datas[currentClickCount].text, datas[currentClickCount].sprite, datas[currentClickCount].bTime);

        SoundManager.Instance.SetAndPlaySFX(datas[currentClickCount].sfxName);

        if (currentClickCount < datas.Length - 1)
        {
            ++currentClickCount;
        }
    }

    public void ChangeSprite(Sprite sprite, int index)
    {
        datas[index].sprite = sprite;
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