using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDissolve : MonoBehaviour
{
    [SerializeField]
    private float mStartDelayTime = 1.0f;
    [SerializeField]
    private float mDissolveSpeed = 0.5f;
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

    private void OnDestroy()
    {
        mMaterial.SetFloat("_Level", 1.0f);
    }
}
