using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Question : MonoBehaviour
{
    private bool isEndQuestion;
    public bool IsEndQuestion
    {
        get { return isEndQuestion; }
        set { isEndQuestion = value; }
    }

    public abstract bool StartQuestion();
    public abstract bool EndQuestion();
}
