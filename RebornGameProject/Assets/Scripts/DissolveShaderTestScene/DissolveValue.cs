using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 디졸브 셰이더 효과 적용
/// </summary>
public class DissolveValue : MonoBehaviour
{
    private Material mMaterial;

    [SerializeField]
    private float mSpeed = 0.5f;
    
    private void Start()
    {
        mMaterial = GetComponent<Renderer>().material;

        StartCoroutine(ChangeShaderValueCoroutine());
    }

    private IEnumerator ChangeShaderValueCoroutine()
    {
        float deltaVal = 0.0f;

        yield return new WaitForSeconds(1.0f);

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
