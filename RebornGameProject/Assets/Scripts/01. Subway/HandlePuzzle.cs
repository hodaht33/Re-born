using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 권준호
/// 기능 : 손잡이 퍼즐 관리
/// </summary>
public class HandlePuzzle : Puzzle
{
    [SerializeField, Tooltip("손잡이 개수 만큼 써서 사용\n1부터 시작")]
    private string answer = "1423";

    private readonly int[] answerNumber = new int[4];
    private Handle[] handles = new Handle[4];
    private readonly List<int> userAnswer = new List<int>();
    private Canvas canvas;

    private void Start()
    {
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        var handleObjects = transform.RecursiveFind("Handles");
        var answerChar = answer.ToCharArray();
        for (int i = 0; i < 4; i++)
        {
            var handleObject = handleObjects.GetChild(i);
            handles[i] = handleObject.gameObject.AddComponent<Handle>();
            handles[i].Parent = this;
            handles[i].Index = i;
            answerNumber[i] = answerChar[i] - '1';
        }
        canvas.enabled = false;
    }

    private void OnMouseDown()
    {
        if (IsEndPuzzle) return;
        canvas.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.enabled = false;
        }
    }

    public void OnHandlePulled(int index)
    {
        userAnswer.Add(index);
        if (userAnswer.Count == 4)
        {
            bool isAnswerCorrect = true;
            for (int i = 0; i < 4; i++)
            {
                handles[i].Release();
                if (userAnswer[i] != answerNumber[i])
                {
                    isAnswerCorrect = false;
                }
            }
            if (isAnswerCorrect)
            {
                IsEndPuzzle = true;
                canvas.enabled = false;
            }
            userAnswer.Clear();
        }
    }

    public override void EndPuzzle()
    {
    }

    public override void StartPuzzle()
    {
    }
}
