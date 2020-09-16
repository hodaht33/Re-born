using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockScript : MonoBehaviour
{
    private int size = 5;
    private string answer = "LOVER";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < size; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.GetComponent<LockTxt>().getAlphabet() != answer[i])
                return;
        }

        SceneManager.LoadScene("End");
    }
}
