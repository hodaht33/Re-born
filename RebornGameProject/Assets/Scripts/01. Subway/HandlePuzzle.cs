using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 권준호
/// 기능 : 손잡이 퍼즐 관리, 손잡이 gameObject에 동적으로 스크립트를 추가하고 이벤트를 받아 옴.
/// 손잡이가 모두 당겨졌을 때, 지정된 순서대로 당겨졌다면 그냥 종료하고 그렇지 않다면 그냥 원상복구함.
/// 한번 손잡이를 모두 당기면 그 이후로는 이벤트가 발생하지 않음.
/// 
/// 수정자 : 곽진성
/// 수정 : 핸들 손잡이 해결 후 퍼즐 결과에 반영
/// </summary>

public class HandlePuzzle : Puzzle
{
    [SerializeField, Tooltip("손잡이를 당기는 순서를 나타내는 4자리 비밀번호. (e.g. 1234,1324,1432)")]
    private string answer = "1423";

    private readonly int[] answerNumber = new int[4];
    private readonly Handle[] handles = new Handle[4];
    private readonly List<int> userAnswer = new List<int>();
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
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

    public void DoPuzzle()
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
                canvas.enabled = false;

                // 퍼즐 완료 반영
                SetPuzzleEnd();
            }
            userAnswer.Clear();
        }
    }
}
