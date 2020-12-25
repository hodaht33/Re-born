#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class FlashLight : MonoBehaviour
{
    [SerializeField]
    private Material reveal;
    [SerializeField]
    private Light flashLight;
    // TODO: 북마크에 있는 46분짜리 기초 라이팅 셰이더 유튜브영상 보고 수정해보고 안되면 Physics.SphereCast사용하여 부자연스럽게 구현

    private void Update()
    {
        reveal.SetVector("_LightPosition", flashLight.transform.position);
        reveal.SetVector("_LightDirection", -flashLight.transform.forward);
        reveal.SetFloat("_LightAngle", flashLight.spotAngle);
        reveal.SetFloat("_LightRange", flashLight.range);
        reveal.SetFloat("_LightEnabled", flashLight.enabled ? 1 : 0);
    }
}
