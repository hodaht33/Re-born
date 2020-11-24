using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public class SubwayLevel : LevelManager
{
    public override bool StartLevel()
    {
        GetCurrentQuestion().StartQuestion();

        return true;
    }
}
