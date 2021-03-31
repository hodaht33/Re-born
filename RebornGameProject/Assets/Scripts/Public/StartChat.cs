#pragma warning disable CS0649

using UnityEngine;

/// <summary>
/// �ۼ��� : �̼�ȣ
/// ��� : ��ȭâ ����
/// </summary>
public class StartChat : MonoBehaviour
{
    // ��ȭâ ���⿡ �ʿ��� ������ ����ü
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
    public ItemLSH item;    // �������� ���� ��ȭâ�� ��� �� �ʿ�

    private int mCurrentClickCount = 0;

    // EventTrigger����� ��� ���
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

        if (mCurrentClickCount < mDatas.Length - 1)
        {
            ++mCurrentClickCount;
        }
    }

    // Collider�� �־� ��� �� �� �ִ� 3D������Ʈ�� ���
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

        if (mCurrentClickCount < mDatas.Length - 1)
        {
            ++mCurrentClickCount;
        }
    }

    public void ChangeSprite(Sprite sprite, int index)
    {
        mDatas[index].sprite = sprite;
    }
}