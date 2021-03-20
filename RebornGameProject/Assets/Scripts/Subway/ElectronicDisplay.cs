using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 전광판 문제 힌트주는 디스플레이 동작 관리
/// 알파 값대신 Emission을 통해 좀 더 밝게 되도록하며 켜고 끄고를 관리
/// 
/// **현재 쓰이지 않음**
/// 
/// </summary>
public class ElectronicDisplay : MonoBehaviour
{
    [SerializeField]
    private float mChangColorSpeed = 3.0f;
    private RaycastHit mHit;
    private int mLayerMask;
    private Material mMat;
    private Coroutine mCoroutine;
    private bool mbRaycast;
    public bool IsRaycast
    {
        get
        {
            return mbRaycast;
        }
        set
        {
            // 서로 반대인 bool값을 가질 때만 Emission값을 변경하여 켜고 끄고 관리
            if (mbRaycast != value)
            {
                mbRaycast = value;

                if (mbRaycast == true)
                {
                    if (mCoroutine != null)
                    {
                        StopCoroutine(mCoroutine);
                    }
                    mCoroutine = StartCoroutine(StartEmissionColor());
                }
                else
                {
                    if (mCoroutine != null)
                    {
                        StopCoroutine(mCoroutine);
                    }
                    mCoroutine = StartCoroutine(EndEmissionColor());
                }
            }
        }
    }

    private void Awake()
    {
        // Player레이어만 레이캐스팅하기위한 레이어 마스크
        mLayerMask = 1 << LayerMask.NameToLayer("Player");  

        mMat = GetComponent<Renderer>().material;
        mMat.EnableKeyword("_EMISSION");
    }

    private void Update()
    {
        // 넓은 범위로 레이캐스팅하기위해 BoxCast사용
        if (Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, -transform.forward, out mHit, transform.rotation, 50.0f, mLayerMask))
        {
            IsRaycast = true;
        }
        else
        {
            IsRaycast = false;
        }
    }

    private IEnumerator StartEmissionColor()
    {
        Color c = mMat.GetColor("_EmissionColor");
        while (c.r < 1.0f)
        {
            c.r = c.g = c.b += mChangColorSpeed * Time.deltaTime;
            mMat.SetColor("_EmissionColor", c);

            yield return null;
        }
    }

    private IEnumerator EndEmissionColor()
    {
        Color c = mMat.GetColor("_EmissionColor");
        while (c.r > 0.0f)
        {
            c.r = c.g = c.b -= mChangColorSpeed * Time.deltaTime;
            mMat.SetColor("_EmissionColor", c);
            
            yield return null;
        }
    }
}
