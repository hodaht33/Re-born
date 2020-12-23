using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 거미줄 클릭 시 소멸 및 단서 획득
/// </summary>
public class Cobweb : MonoBehaviour
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
        if (mDisappearCoroutine != null)
        {
            StopCoroutine(mDisappearCoroutine);
        }
        mDisappearCoroutine = StartCoroutine(Disappear());
        Inventory.Instance.GetItem(mItemPrefab);
    }

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
}
