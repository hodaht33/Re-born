using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 이미지 메테리얼의 디졸브 셰이더 값 변경하여 디졸브 효과 적용
/// </summary>
public class DissolveImage : MonoBehaviour
{
    private Material mMaterial;

    [SerializeField]
    private float mSpeed = 0.5f;    // 값 변경 적용 속도

    private Coroutine mDissolveCoroutine;

    public Coroutine StartDissolve()
    {
        return mDissolveCoroutine = StartCoroutine(ChangeShaderValueCoroutine());
    }

    public void SetDefault()
    {
        if (mDissolveCoroutine != null)
        {
            StopCoroutine(mDissolveCoroutine);
        }

        mMaterial.SetFloat("_Level", 0.0f);
        mMaterial.SetFloat("_Edges", 0.0f);
    }

    private void Awake()
    {
        mMaterial = GetComponent<Image>().material;
    }

    private IEnumerator ChangeShaderValueCoroutine()
    {
        float deltaVal = 0.0f;

        while (deltaVal < 0.1f)
        {
            deltaVal += Time.deltaTime * mSpeed;
            mMaterial.SetFloat("_Edges", deltaVal);

            yield return null;
        }

        deltaVal = 0.0f;

        while (deltaVal <= 1.0f)
        {
            deltaVal += Time.deltaTime * mSpeed;
            mMaterial.SetFloat("_Level", deltaVal);

            yield return null;
        }
    }
}
