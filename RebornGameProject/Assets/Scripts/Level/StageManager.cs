using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonBase<StageManager>
{
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public delegate void ClearStage();
    public event ClearStage clearSubway;
    public event ClearStage clearCampus;
    public event ClearStage clearClassroom;

    public void ClearSubway()
    {
        clearSubway.Invoke();
    }

    public void ClearCampus()
    {
        clearCampus.Invoke();
    }

    public void ClearClassroom()
    {
        clearClassroom.Invoke();
    }
}
