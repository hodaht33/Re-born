#pragma warning disable CS0649

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : DissolveShaderTest씬의 테스트 기능
/// </summary>
public class DissolveShaderTestList : MonoBehaviour
{
    private Material[] mMaterials;
    private GameObject[] mImageObjects;

    [SerializeField]
    private GameObject[] mTextsObjects;

    [SerializeField]
    private float mSpeed = 0.5f;

    [SerializeField]
    private Text mText;

    private int mCurrentEffectNum;
    private Coroutine mCurrentCoroutine;

    private void Awake()
    {
        Image[] img = GetComponentsInChildren<Image>();

        mMaterials = new Material[img.Length];
        mImageObjects = new GameObject[img.Length];
        for (int i = 0; i < img.Length; ++i)
        {
            mMaterials[i] = img[i].material;
            mImageObjects[i] = img[i].gameObject;
            mImageObjects[i].SetActive(false);

            mMaterials[i].SetFloat("_Edges", 0.0f);
            mMaterials[i].SetFloat("_Level", 0.0f);
        }
        mImageObjects[0].SetActive(true);
        mCurrentCoroutine = StartCoroutine(ChangeShaderValueCoroutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (mCurrentCoroutine != null)
            {
                StopCoroutine(mCurrentCoroutine);
                mMaterials[mCurrentEffectNum].SetFloat("_Edges", 0.0f);
                mMaterials[mCurrentEffectNum].SetFloat("_Level", 0.0f);
            }

            mImageObjects[mCurrentEffectNum].SetActive(false);

            if (mCurrentEffectNum != 0)
            {
                --mCurrentEffectNum;
            }

            mText.text = (mCurrentEffectNum + 1).ToString();
            mImageObjects[mCurrentEffectNum].SetActive(true);

            mMaterials[mCurrentEffectNum].SetFloat("_Edges", 0.0f);
            mMaterials[mCurrentEffectNum].SetFloat("_Level", 0.0f);

            mCurrentCoroutine = StartCoroutine(ChangeShaderValueCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (mCurrentCoroutine != null)
            {
                StopCoroutine(mCurrentCoroutine);
                mMaterials[mCurrentEffectNum].SetFloat("_Edges", 0.0f);
                mMaterials[mCurrentEffectNum].SetFloat("_Level", 0.0f);
            }

            mImageObjects[mCurrentEffectNum].SetActive(false);

            if (mCurrentEffectNum != mMaterials.Length - 1)
            {
                ++mCurrentEffectNum;
            }

            mText.text = (mCurrentEffectNum + 1).ToString();
            mImageObjects[mCurrentEffectNum].SetActive(true);

            mMaterials[mCurrentEffectNum].SetFloat("_Edges", 0.0f);
            mMaterials[mCurrentEffectNum].SetFloat("_Level", 0.0f);

            mCurrentCoroutine = StartCoroutine(ChangeShaderValueCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (mCurrentCoroutine != null)
            {
                StopCoroutine(mCurrentCoroutine);
            }

            mMaterials[mCurrentEffectNum].SetFloat("_Edges", 0.0f);
            mMaterials[mCurrentEffectNum].SetFloat("_Level", 0.0f);

            mCurrentCoroutine = StartCoroutine(ChangeShaderValueCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            bool activate = false;

            if (mTextsObjects[0].activeInHierarchy == false)
            {
                activate = true;
            }

            for (int i = 0; i < mTextsObjects.Length; ++i)
            {
                mTextsObjects[i].SetActive(activate);
            }
        }
    }

    private IEnumerator ChangeShaderValueCoroutine()
    {
        float deltaVal = 0.0f;

        yield return new WaitForSeconds(1.0f);

        while (deltaVal < 0.1f)
        {
            deltaVal += Time.deltaTime * mSpeed;
            mMaterials[mCurrentEffectNum].SetFloat("_Edges", deltaVal);

            yield return null;
        }

        deltaVal = 0.0f;

        while (deltaVal <= 1.0f)
        {
            deltaVal += Time.deltaTime * mSpeed;
            mMaterials[mCurrentEffectNum].SetFloat("_Level", deltaVal);

            yield return null;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < mMaterials.Length; ++i)
        {
            mMaterials[i].SetFloat("_Edges", 0.0f);
            mMaterials[i].SetFloat("_Level", 0.0f);
        }
    }
}
