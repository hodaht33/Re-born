using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 문제 해결 여부에 따른 기능 제한
/// </summary>
public class TreeQuestionSystem : SingletonBase<TreeQuestionSystem>, IQuestion
{
    // TODO : IQuestionSystem이 필요한 곳은 FallingTreeSequence같음
    // TODO : QuestionManager에서 IQuestion을 가지는 List를 두고 모든 Question을 해결하면 NextLevel로 가도록 LevelManager에 알리는 방식
    // TODO : 이 스크립트는 QuestionManager에서 공통적으로 가질 것들만 옮기는 방식으로 수행
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
    }
    
    public void SuccessQuestion()
    {
        playerMove.enabled = true;
    }

    public bool StartQuestion()
    {
        playerMove.enabled = false;

        return true;
    }

    public bool EndQuestion()
    {
        playerMove.enabled = true;

        return true;
    }
}
