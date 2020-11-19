using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayLevel : LevelManager
{
    public override bool StartLevel()
    {
        GetCurrentQuestion().StartQuestion();

        return true;
    }
}
