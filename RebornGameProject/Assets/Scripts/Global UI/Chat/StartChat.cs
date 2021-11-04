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
        // UI 패널이 활성화되어있으면 리턴
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        mData data = mDatas[clickCount];
        Chat.Instance.ActivateChat(data.text, data.sprite, data.bTime);

        if (clickCount < mDatas.Length - 1)
        {
            ++clickCount;
        }
    }
}