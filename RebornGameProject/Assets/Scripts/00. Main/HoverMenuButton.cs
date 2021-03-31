using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : MainMenu씬 버튼UI에 마우스 오버 시 밑줄 이미지로 변경
/// </summary>
public class HoverMenuButton : MonoBehaviour
{
    private Image mImage;
    [SerializeField]
    private Sprite mDefaultSpriteImg;
    [SerializeField]
    private Sprite mUnderlineSpriteImg;

    private void Awake()
    {
        mImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnMouseEnter()
    {
        mImage.sprite = mUnderlineSpriteImg;
    }

    public void OnMouseExit()
    {
        mImage.sprite = mDefaultSpriteImg;
    }
}
