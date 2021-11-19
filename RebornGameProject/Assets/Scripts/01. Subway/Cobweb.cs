#pragma warning disable CS0649

using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 거미줄 클릭 시 소멸 및 단서 획득
/// </summary>
public class Cobweb : Puzzle
{
    [SerializeField]
    private float mDisappearSpeed = 3.0f;
    [SerializeField]
    private ItemLSH mItemPrefab;
    private Material mMat;
    private Coroutine mDisappearCoroutine;

    private void Awake()
    {
        mMat = GetComponent<Renderer>().material;
    }

    private void OnMouseUp()
    {
        if (IsEndPuzzle == false)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            SetPuzzleEnd();
            //EndPuzzle();
        }
    }

    // 알파값 조절하여 없어지게 만드는 코루틴
    private IEnumerator Disappear()
    {
        Color c = mMat.color;
        while (mMat.color.a > 0.0f)
        {
            c.a -= mDisappearSpeed * Time.deltaTime;
            mMat.color = c;

            yield return null;
        }
        c.a = 0.0f;
        mMat.color = c;
    }

    //public override void StartPuzzle()
    //{
    //    throw new System.NotImplementedException();
    //}

    //// 퍼즐 종료 메서드
    //public override void EndPuzzle()
    //{
    //    if (mDisappearCoroutine != null)
    //    {
    //        StopCoroutine(mDisappearCoroutine);
    //    }
    //    mDisappearCoroutine = StartCoroutine(Disappear());

    //    // 기획서 상 어떠한 아이템을 얻게 하기로 했으나 정해진게 없음
    //    // 기획자분들께 물어봐야 함
    //    Inventory.Instance.GetItem(mItemPrefab);
    //    IsEndPuzzle = true;
    //}
}
