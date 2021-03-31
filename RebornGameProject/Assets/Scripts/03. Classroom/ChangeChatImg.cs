#pragma warning disable CS0649

using UnityEngine;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 클릭시 Chat 스크립트의 img 변경
/// </summary>

public class ChangeChatImg : MonoBehaviour
{
    [SerializeField]
    private StartChat mStartChat;
    [SerializeField]
    private Sprite mSprite;
    [SerializeField]
    private int mIndex;

    private void OnMouseDown()
    {
        if (mStartChat != null)
        {
            mStartChat.ChangeSprite(mSprite, 0);
        }
    }
}
