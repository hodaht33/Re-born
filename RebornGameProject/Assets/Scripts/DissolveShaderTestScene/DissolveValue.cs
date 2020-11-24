using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveValue : MonoBehaviour
{
    private Material mat;

    [SerializeField]
    private float speed = 0.5f;
    
    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        //mat.shader = Shader.Find("Unlit/Dissolve");

        StartCoroutine(ChangeShaderValue());
    }

    private IEnumerator ChangeShaderValue()
    {
        float deltaVal = 0.0f;

        yield return new WaitForSeconds(1.0f);

        while (deltaVal < 0.1f)
        {
            deltaVal += Time.deltaTime * speed;
            mat.SetFloat("_Edges", deltaVal);
            
            yield return null;
        }

        deltaVal = 0.0f;

        while (deltaVal <= 1.0f)
        {
            deltaVal += Time.deltaTime * speed;
            mat.SetFloat("_Level", deltaVal);

            yield return null;
        }
    }
}
