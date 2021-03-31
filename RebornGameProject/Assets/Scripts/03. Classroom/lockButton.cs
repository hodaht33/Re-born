using UnityEngine;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 자물쇠 버튼 클릭 기능
/// </summary>

public class lockButton : MonoBehaviour
{
    private LockTxt mLockTxt = null;
    private int mButtonClickCount = 0;

    public delegate void Click();
    public event Click click;

    public void ButtonUp()
    {
        ++mButtonClickCount;

        if (mButtonClickCount >= mLockTxt.AlphabetLength)
        {
            mButtonClickCount = 0;
        }

        mLockTxt.ChangeAlphabet(mButtonClickCount);
        click.Invoke();
    }

    public void ButtonDown()
    {
        --mButtonClickCount;

        if (mButtonClickCount < 0)
        {
            mButtonClickCount = mLockTxt.AlphabetLength - 1;
        }

        mLockTxt.ChangeAlphabet(mButtonClickCount);
        click.Invoke();
    }

    private void Awake()
    {
        mLockTxt = GetComponent<LockTxt>();
    }
}
