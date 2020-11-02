using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeQuestionSystem : SingletonBase<TreeQuestionSystem>
{
    private bool isSuccess;
    public bool Success
    {
        get { return isSuccess; }
        set { isSuccess = value; }
    }

    [SerializeField]
    private PlayerMove playerMove;
    
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

        playerMove.enabled = false;
    }
    
    public void SuccessQuestion()
    {
        playerMove.enabled = true;
    }
}
