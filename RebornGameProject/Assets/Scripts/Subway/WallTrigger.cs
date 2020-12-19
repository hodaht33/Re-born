#pragma warning disable CS0649

using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 지하철 벽 투명화(셰이더 값 변경)
/// </summary>
public class WallTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject mWall;
    private Material[] mMaterials;
    private Coroutine mCoroutine = null;

    private void Awake()
    {
        mMaterials = mWall.GetComponent<Renderer>().materials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            if (mCoroutine != null)
            {
                StopCoroutine(mCoroutine);
                mCoroutine = null;
            }

            mCoroutine = StartCoroutine(ChangeAlphaToHalfCoroutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            if (mCoroutine != null)
            {
                StopCoroutine(mCoroutine);
                mCoroutine = null;
            }

            mCoroutine = StartCoroutine(ChangeAlphaToOne());
        }
    }

    private IEnumerator ChangeAlphaToHalfCoroutine()
    {
        Color[] colors = new Color[mMaterials.Length];
        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i] = mMaterials[i].color;
            mMaterials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mMaterials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mMaterials[i].SetInt("_ZWrite", 0);
            mMaterials[i].DisableKeyword("_ALPHATEST_ON");
            mMaterials[i].DisableKeyword("_ALPHABLEND_ON");
            mMaterials[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
            mMaterials[i].renderQueue = 3000;
        }

        while (colors[0].a > 0.3f)
        {
            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i].a -= Time.deltaTime * 1.0f;
                mMaterials[i].color = colors[i];
            }

            yield return null;
        }

        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i].a = 0.3f;
            mMaterials[i].color = colors[i];
        }
    }

    private IEnumerator ChangeAlphaToOne()
    {
        Color[] colors = new Color[mMaterials.Length];
        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i] = mMaterials[i].color;
        }

        while (colors[0].a < 1.0f)
        {
            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i].a += Time.deltaTime * 1.0f;
                mMaterials[i].color = colors[i];
            }

            yield return null;
        }

        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i].a = 1.0f;
            mMaterials[i].color = colors[i];

            mMaterials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            mMaterials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            mMaterials[i].SetInt("_ZWrite", 1);
            mMaterials[i].DisableKeyword("_ALPHATEST_ON");
            mMaterials[i].DisableKeyword("_ALPHABLEND_ON");
            mMaterials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mMaterials[i].renderQueue = -1;
        }
    }
}
