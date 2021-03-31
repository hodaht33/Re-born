#pragma warning disable CS0649

using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 이미지를 비추는 손전등(손전등 퍼즐)
/// </summary>
//[ExecuteInEditMode]
public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private Material reveal;
    [SerializeField]
    private Light flashLight;

    // reveal메테리얼에 적용된 reveal셰이더에 값 전달
    private void Update()
    {
        reveal.SetVector("_LightPosition", flashLight.transform.position);
        reveal.SetVector("_LightDirection", -flashLight.transform.forward);
        reveal.SetFloat("_LightAngle", flashLight.spotAngle);
        reveal.SetFloat("_LightRange", flashLight.range);
        reveal.SetFloat("_LightEnabled", flashLight.enabled ? 1 : 0);
    }
}
