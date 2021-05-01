using UnityEngine;
using UnityEngine.UI;

public class LockNumber : MonoBehaviour
{
    public delegate void NumberUpdatedCallback(LockNumber lockNumber);

    private Text text;
    private int number = 0;
    public int Index = 0;
    public int Min = 0;
    public int Max = 9;
    public int Number
    {
        get => number;
        set
        {
            number = value;
            if (number > Max) number = Min;
            if (number < Min) number = Max;
            text.text = number + "";
            OnNumberUpdated?.Invoke(this);
        }
    }
    public NumberUpdatedCallback OnNumberUpdated;

    public void OnUpClicked()
    {
        Number++;
    }

    public void OnDownClicked()
    {
        Number--;
    }

    // Start is called before the first frame update
    void Start()
    {
        text = transform.RecursiveFind("Text").GetComponent<Text>();
    }
}
