using UnityEngine;

public class lockButton : MonoBehaviour
{
    private LockTxt lockTxt = null;
    private int buttonClickCount = 0;

    public delegate void Click();
    public event Click click;

    private void Awake()
    {
        lockTxt = GetComponent<LockTxt>();
    }

    public void ButtonUp()
    {
        ++buttonClickCount;

        if (buttonClickCount >= lockTxt.AlphabetLength)
        {
            buttonClickCount = 0;
        }

        lockTxt.ChangeAlphabet(buttonClickCount);
        click.Invoke();
    }

    public void ButtonDown()
    {
        --buttonClickCount;

        if (buttonClickCount < 0)
        {
            buttonClickCount = lockTxt.AlphabetLength - 1;
        }

        lockTxt.ChangeAlphabet(buttonClickCount);
        click.Invoke();
    }
}
