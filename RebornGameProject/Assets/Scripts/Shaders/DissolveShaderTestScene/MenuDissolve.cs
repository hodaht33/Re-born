using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : Menu UI에 dissolve 셰이더 효과 적용
/// </summary>
public class MenuDissolve : MonoBehaviour
{
    [SerializeField]
    private float mStartDelayTime = 1.0f;   // 시작 지연 시간
    [SerializeField]
    private float mDissolveSpeed = 0.5f;    // 셰이더 적용 속도
    [SerializeField, Tooltip("타이틀 이미지 넣기")]
    private Image mImageForGetDissolveMaterial;
    [SerializeField]
    private Image[] mImageButtons;
    private Material mMaterial;
    private bool isEndDissolve;
    public bool IsEndDissolve
    {
        get
        {
            return isEndDissolve;
        }
        private set { }
    }

    public IEnumerator ChangeShaderValueCoroutine()
    {
        float deltaVal = 1.0f;

        yield return new WaitForSeconds(mStartDelayTime);

        while (deltaVal > 0.01f)
        {
            deltaVal -= Time.deltaTime * mDissolveSpeed;

            mMaterial.SetFloat("_Level", deltaVal);

            yield return null;
        }

        // 셰이더 효과 끝난 후에 UI클릭 가능하도록 하기 위함(Inspector에서 raycastTarget을 꺼둠)
        foreach (Image img in mImageButtons)
        {
            img.raycastTarget = true;
        }

        isEndDissolve = true;
    }

    private void Awake()
    {
        mMaterial = mImageForGetDissolveMaterial.GetComponent<Image>().material;

        StartCoroutine(ChangeShaderValueCoroutine());
    }

    // 종료 시 다시 초기화시킴
    private void OnDestroy()
    {
        mMaterial.SetFloat("_Level", 1.0f);
    }
}
