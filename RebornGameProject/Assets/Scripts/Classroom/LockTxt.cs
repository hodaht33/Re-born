#pragma warning disable CS0649

using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 자물쇠 text 출력
/// </summary>

public class LockTxt : MonoBehaviour
{
    [SerializeField]
    private char[] mAlphabet;
    public int AlphabetLength
    {
        get
        {
            return mAlphabet.Length;
        }
        private set
        {

        }
    }

    private char mCurrentAlphabet;
    private Text mLockAlpha;

    private void Awake()
    {
        mLockAlpha = GetComponent<Text>();
    }

    private void Start()
    {
        ChangeAlphabet(0);
    }

    public void ChangeAlphabet(int i)
    {
        mLockAlpha.text = mAlphabet[i].ToString();
        mCurrentAlphabet = mAlphabet[i];
    }

    public char GetAlphabet()
    {
        return mCurrentAlphabet;
    }
}
