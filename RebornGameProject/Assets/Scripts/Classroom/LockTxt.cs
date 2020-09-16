using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockTxt : MonoBehaviour
{
    public char[] alphabet = new char[7];
    public int i = 0;
    public Text lockAlpha;

    // Start is called before the first frame update
    void Start()
    {
        ChangeAlphabet();
    }

    public void ChangeAlphabet()
    {
        lockAlpha.text = alphabet[i].ToString();
    }

    public char getAlphabet() { return alphabet[i]; }
}
