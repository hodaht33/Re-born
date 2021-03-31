#pragma warning disable CS0649
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 손잡이 퍼즐 관리
/// 이 퍼즐은 임시방편으로 만들었으며 3D모델도 필요(기획자분들과 상의해야 함)
/// </summary>
public class HandlePuzzle : Puzzle
{
    [SerializeField]
    private Handle mHandlePrefab;
    [SerializeField, Tooltip("손잡이 개수 만큼 써서 사용\n1부터 시작")]
    private string[] mAnswers;  // 정답 순번이 차례대로 들어감
    private int mAnswerIndex;
    private Handle[] handles;   // 게임 시작 시 각각의 손잡이 오브젝트들이 담김
    private bool mRightAnswer;  // 정답 여부

    // 정답 확인 메서드
    public void CheckAnswer(Handle handle)
    {
        // 손잡이 오브젝트의 이름을 숫자로 두므로 손잡이 오브젝트의 이름과 비교하여
        // 같지 않으면 틀렸다고 설정
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

    // 퍼즐에 필요한 변수 초기화하는 메서드
    public override void StartPuzzle()
    {
        mRightAnswer = true;
        mAnswerIndex = 0;
        for (int i = 0; i < handles.Length; ++i)
        {
            handles[i].IsPull = false;
        }
    }

    // 정답 시 더이상 퍼즐이 진행되지 않음
    public override void EndPuzzle()
    {
        //for (int i = 0; i < handles.Length; ++i)
        //{
        //    handles[i].enabled = false;
        //}
        IsEndPuzzle = true;
    }

    private void Awake()
    {
        // 손잡이 오브젝트들 생성
        handles = new Handle[mAnswers.Length];
        for (int i = 0; i < mAnswers.Length; ++i)
        {
            Handle handle = Instantiate(mHandlePrefab,
                new Vector3(transform.position.x, transform.position.y, transform.position.z + (3 * i)),
                transform.rotation,
                transform);
            handle.OnCheckAnswer += CheckAnswer;
            handle.name = (i + 1).ToString();
            handles[i] = handle;
        }
        StartPuzzle();
    }
}
