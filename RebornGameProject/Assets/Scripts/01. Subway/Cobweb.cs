#pragma warning disable CS0649

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
    private Material mMat;
    private bool succeed = false;

    private void Awake()
    {
        mMat = GetComponent<Renderer>().material;
    }

    private void OnMouseDown()
    {
        if (succeed) return;

        succeed = true;
        StartCoroutine(Disappear());
    }

    // 알파값 조절하여 없어지게 만드는 코루틴
    private IEnumerator Disappear()
    {
        Color c = mMat.color;
        
        while (mMat.color.a > 0.0f)
        {
            c.a -= mDisappearSpeed * Time.deltaTime;
            mMat.SetColor("_BaseColor", new Color(mMat.color.r, mMat.color.g, mMat.color.b, c.a));

            yield return null;
        }
        c.a = 0.0f;
        mMat.color = c;
        mMat.SetColor("_BaseColor", new Color(mMat.color.r, mMat.color.g, mMat.color.b, c.a));
    }
}
