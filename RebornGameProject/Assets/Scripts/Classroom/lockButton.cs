using UnityEngine;

public class lockButton : MonoBehaviour
{
    private LockTxt lockTxt = null;
    private int buttonClickCount = 0;

    public delegate void Click();
    public event Click click;

    private void Awake()
    {
        lockTxt = transform.parent.GetComponent<LockTxt>();
    }

    public void ButtonUp()
    {
        ++buttonClickCount;

        if (buttonClickCount > 6)
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
            buttonClickCount = 6;
        }

        lockTxt.ChangeAlphabet(buttonClickCount);
    }
}
