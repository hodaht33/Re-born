using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DissolveShaderTestList : MonoBehaviour
{
    private Material[] mat;
    private GameObject[] imageObject;

    [SerializeField]
    private GameObject[] textsObject;

    [SerializeField]
    private float speed = 0.5f;

    [SerializeField]
    private Text text;

    private int currentEffectNum = 0;
    private Coroutine currentCoroutine = null;

    private void Awake()
    {
        Image[] img = GetComponentsInChildren<Image>();

        mat = new Material[img.Length];
        imageObject = new GameObject[img.Length];
        for (int i = 0; i < img.Length; ++i)
        {
            mat[i] = img[i].material;
            imageObject[i] = img[i].gameObject;
            imageObject[i].SetActive(false);

            mat[i].SetFloat("_Edges", 0.0f);
            mat[i].SetFloat("_Level", 0.0f);
        }
        imageObject[0].SetActive(true);
        currentCoroutine = StartCoroutine(ChangeShaderValue());
    }

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
    }

    private IEnumerator ChangeShaderValue()
    {
        float deltaVal = 0.0f;

        yield return new WaitForSeconds(1.0f);

        while (deltaVal < 0.1f)
        {
            deltaVal += Time.deltaTime * speed;
            mat[currentEffectNum].SetFloat("_Edges", deltaVal);

            yield return null;
        }

        deltaVal = 0.0f;

        while (deltaVal <= 1.0f)
        {
            deltaVal += Time.deltaTime * speed;
            mat[currentEffectNum].SetFloat("_Level", deltaVal);

            yield return null;
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < mat.Length; ++i)
        {
            mat[i].SetFloat("_Edges", 0.0f);
            mat[i].SetFloat("_Level", 0.0f);
        }
    }
}
