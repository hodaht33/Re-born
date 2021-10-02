#pragma warning disable CS0649

using UnityEngine;

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

    private int clickCount = 0;

    public void OnMouseDown()
    {
        mData data = mDatas[clickCount];
        Chat.Instance.ActivateChat(data.text, data.sprite, data.bTime);

        if (clickCount < mDatas.Length - 1)
        {
            ++clickCount;
        }
    }
}