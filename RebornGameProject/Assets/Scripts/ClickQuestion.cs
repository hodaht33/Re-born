using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public class ClickQuestion : Question
{
    public override bool EndQuestion()
    {
        return true;
    }

    public override bool StartQuestion()
    {
        throw new System.NotImplementedException();
    }

    private void OnMouseDown()
    {
        transform.parent.GetComponent<LevelManager>().SuccessCurrentQuestion();
    }
}
