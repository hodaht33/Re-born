using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 아이템 사용 문제
/// </summary>
public class UseItemQuestion : Question
{
    [SerializeField]
    private ItemLSH item;
    [SerializeField]
    private Sprite solvedSprite;
    [SerializeField]
    private ChatImageClick chatImageClick;
    
    protected override void Solve()
    {
        if (mbSolve == true)
        {
            Debug.Log("오류 : 이미 풀린 문제입니다.");

            return;
        }

        if (Inventory.Instance.UseSelectedItem(item.ItemName))
        {


            mbSolve = true;
        }
    }
}
