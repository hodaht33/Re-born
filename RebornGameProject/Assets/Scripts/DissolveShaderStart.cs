using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DissolveShaderStart : MonoBehaviour
{
    private Material mat;

    [SerializeField]
    private float speed = 0.5f;
    
    private void Awake()
    {
        mat = GetComponent<Text>().material;
    }

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                mat[currentEffectNum].SetFloat("_Edges", 0.0f);
                mat[currentEffectNum].SetFloat("_Level", 0.0f);
            }

            imageObject[currentEffectNum].SetActive(false);

            if (currentEffectNum != 0)
            {
                --currentEffectNum;
            }

            text.text = (currentEffectNum + 1).ToString();
            imageObject[currentEffectNum].SetActive(true);

            mat[currentEffectNum].SetFloat("_Edges", 0.0f);
            mat[currentEffectNum].SetFloat("_Level", 0.0f);

            currentCoroutine = StartCoroutine(ChangeShaderValue());
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                mat[currentEffectNum].SetFloat("_Edges", 0.0f);
                mat[currentEffectNum].SetFloat("_Level", 0.0f);
            }

            imageObject[currentEffectNum].SetActive(false);

            if (currentEffectNum != mat.Length - 1)
            {
                ++currentEffectNum;
            }

            text.text = (currentEffectNum + 1).ToString();
            imageObject[currentEffectNum].SetActive(true);

            mat[currentEffectNum].SetFloat("_Edges", 0.0f);
            mat[currentEffectNum].SetFloat("_Level", 0.0f);

            currentCoroutine = StartCoroutine(ChangeShaderValue());
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }

            mat[currentEffectNum].SetFloat("_Edges", 0.0f);
            mat[currentEffectNum].SetFloat("_Level", 0.0f);

            currentCoroutine = StartCoroutine(ChangeShaderValue());
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            bool activate = false;

            if (textsObject[0].activeInHierarchy == false)
            {
                activate = true;
            }

            for (int i = 0; i < textsObject.Length; ++i)
            {
                textsObject[i].SetActive(activate);
            }
        }
    }*/

    public IEnumerator ChangeShaderValue()
    {
        float deltaVal = 1.0f;

        yield return new WaitForSeconds(1.0f);

        while (deltaVal > 0.01f)
        {
            deltaVal -= Time.deltaTime * speed;
            mat.SetFloat("_Level", deltaVal);

            yield return null;
        }

        //deltaVal = 0.0f;

        //while (deltaVal <= 1.0f)
        //{
        //    deltaVal += Time.deltaTime * speed;
        //    mat[currentEffectNum].SetFloat("_Level", deltaVal);

        //    yield return null;
        //}
    }

    private void OnDestroy()
    {
        mat.SetFloat("_Edges", 1.0f);
        mat.SetFloat("_Level", 1.0f);
    }
}
