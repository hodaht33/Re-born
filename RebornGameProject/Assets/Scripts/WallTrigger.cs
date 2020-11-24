using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    private Material[] materials;
    private Coroutine coroutine = null;

    private void Awake()
    {
        materials = wall.GetComponent<Renderer>().materials;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }

            coroutine = StartCoroutine(ChangeAlphaToHalf());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }

            coroutine = StartCoroutine(ChangeAlphaToOne());
        }
    }

    private IEnumerator ChangeAlphaToHalf()
    {
        Color[] colors = new Color[materials.Length];
        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i] = materials[i].color;
            materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            materials[i].SetInt("_ZWrite", 0);
            materials[i].DisableKeyword("_ALPHATEST_ON");
            materials[i].DisableKeyword("_ALPHABLEND_ON");
            materials[i].EnableKeyword("_ALPHAPREMULTIPLY_ON");
            materials[i].renderQueue = 3000;
        }

        while (colors[0].a > 0.3f)
        {
            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i].a -= Time.deltaTime * 1.0f;
                materials[i].color = colors[i];
            }

            yield return null;
        }

        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i].a = 0.3f;
            materials[i].color = colors[i];
        }
    }

    private IEnumerator ChangeAlphaToOne()
    {
        Color[] colors = new Color[materials.Length];
        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i] = materials[i].color;
        }

        while (colors[0].a < 1.0f)
        {
            for (int i = 0; i < colors.Length; ++i)
            {
                colors[i].a += Time.deltaTime * 1.0f;
                materials[i].color = colors[i];
            }

            yield return null;
        }

        for (int i = 0; i < colors.Length; ++i)
        {
            colors[i].a = 1.0f;
            materials[i].color = colors[i];

            materials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            materials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            materials[i].SetInt("_ZWrite", 1);
            materials[i].DisableKeyword("_ALPHATEST_ON");
            materials[i].DisableKeyword("_ALPHABLEND_ON");
            materials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
            materials[i].renderQueue = -1;
        }
    }
}
