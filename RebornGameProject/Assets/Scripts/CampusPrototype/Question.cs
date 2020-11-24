using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// TreeQuestion 수정 후에
/// 삭제 예정
/// </summary>
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
