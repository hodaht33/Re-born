using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        mLayerMask = 1 << LayerMask.NameToLayer("Player");
        mMat = GetComponent<Renderer>().material;
        mMat.EnableKeyword("_EMISSION");
    }

    private void Update()
    {
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
