using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public abstract class LevelManager : MonoBehaviour
{
    [SerializeField] private Question[] questions;
    private int currentQuestionIndex;

    private bool isEndLevel;
    public bool IsEndLevel
    {
        get { return isEndLevel; }
        set { isEndLevel = value; }
    }
    
    public string LevelName { get { return name; } }

    [SerializeField] private Transform levelCamPos;
    public Transform LevelCamPos { get { return levelCamPos; } }

    [SerializeField] private LevelManager nextLevel;
    public LevelManager NextLevel { get { return nextLevel; } }
    
    public Question GetCurrentQuestion()
    {
        return questions[currentQuestionIndex];
    }

    public bool SuccessCurrentQuestion()
    {
        ++currentQuestionIndex;

        if (questions.Length == currentQuestionIndex)
        {
            EndLevel();
            nextLevel.StartLevel();

            return true;
        }
        else if (questions.Length < currentQuestionIndex)
        {
            return false;
        }
        else
        {
            questions[currentQuestionIndex].StartQuestion();

            return true;
        }
    }

    public abstract bool StartLevel();
    public virtual bool EndLevel()
    {
        MoveNextLevel();

        return true;
    }

    public void MoveNextLevel()
    {
        Camera.main.GetComponent<CamMoveNextLevel>().enabled = true;
        Camera.main.GetComponent<CamMoveNextLevel>().MovePos(LevelCamPos);
    }
}
