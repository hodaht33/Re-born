using UnityEngine;
using UnityEngine.UI;

public class LockTxt : MonoBehaviour
{
    [SerializeField]
    private char[] alphabet;
    private char currentAlphabet;
    private Text lockAlpha;

    private void Awake()
    {
        lockAlpha = GetComponent<Text>();
    }

    private void Start()
    {
        ChangeAlphabet(0);
    }

    public void ChangeAlphabet(int i)
    {
        lockAlpha.text = alphabet[i].ToString();
        currentAlphabet = alphabet[i];
    }

    public char GetAlphabet()
    {
        return currentAlphabet;
    }
}
