#pragma warning disable CS0649
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 손잡이 퍼즐 관리
/// </summary>
public class HandlePuzzle : Puzzle
{
    [SerializeField]
    private Handle mHandlePrefab;
    [SerializeField, Tooltip("손잡이 개수 만큼 써서 사용\n0부터 시작하도록하여 프로그래밍의 편의를 주세요!")]
    private string[] mAnswers;
    private int mAnswerIndex;
    private Handle[] handles;
    private bool mRightAnswer;
    
    public void CheckAnswer(Handle handle)
    {
        if (mAnswers[mAnswerIndex] != handle.name)
        {
            mRightAnswer = false;
        }
        ++mAnswerIndex;

        if (mAnswerIndex == mAnswers.Length)
        {
            if (mRightAnswer == true)
            {
                EndPuzzle();
            }
            else
            {
                // 틀리면 손잡이 위치 되돌림
                StartPuzzle();
            }
        }
    }

    public override void StartPuzzle()
    {
        mRightAnswer = true;
        mAnswerIndex = 0;
        for (int i = 0; i < handles.Length; ++i)
        {
            handles[i].IsPull = false;
        }
    }

    public override void EndPuzzle()
    {
        //for (int i = 0; i < handles.Length; ++i)
        //{
        //    handles[i].enabled = false;
        //}
    }

    private void Awake()
    {
        handles = new Handle[mAnswers.Length];
        for (int i = 0; i < mAnswers.Length; ++i)
        {
            Handle handle = Instantiate<Handle>(mHandlePrefab,
                new Vector3(transform.position.x + (3 * i), transform.position.y, transform.position.z),
                transform.rotation,
                transform);
            handle.OnCheckAnswer += CheckAnswer;
            handle.name = (i + 1).ToString();
            handles[i] = handle;
        }
        StartPuzzle();
    }
}
