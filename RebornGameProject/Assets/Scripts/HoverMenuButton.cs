using UnityEngine;
using UnityEngine.UI;

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
