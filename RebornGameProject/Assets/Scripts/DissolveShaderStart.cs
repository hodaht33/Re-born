using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DissolveShaderStart : MonoBehaviour
{
    private Material mMaterial;

    [SerializeField]
    private float mSpeed = 0.5f;
    
    public IEnumerator ChangeShaderValueCoroutine()
    {
        float deltaVal = 1.0f;

        yield return new WaitForSeconds(1.0f);

        while (deltaVal > 0.01f)
        {
            deltaVal -= Time.deltaTime * mSpeed;
            mMaterial.SetFloat("_Level", deltaVal);

            yield return null;
        }
    }
    
    private void Awake()
    {
        mMaterial = GetComponent<Text>().material;
    }

    private void OnDestroy()
    {
        mMaterial.SetFloat("_Edges", 1.0f);
        mMaterial.SetFloat("_Level", 1.0f);
    }
}
