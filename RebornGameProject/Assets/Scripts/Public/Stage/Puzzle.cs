using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 퍼즐 해결을 기록하고 매니저에게 반영
/// </summary>
public class Puzzle : MonoBehaviour
{
    // 퍼즐 완성 여부
    public bool IsEndPuzzle
    {
        get;
        set;
    }
    
    [SerializeField] PuzzleHint hint;               // 힌트 이름
    [SerializeField] bool hintCheck = false;    // 힌트가 있는지 여부

    // 게임 시작 시 퍼즐 엔딩은 초기화
    private void Start()
    {
        IsEndPuzzle = false;
    }

    // 퍼즐이 끝났음을 반영
    public void SetPuzzleEnd()
    {
        IsEndPuzzle = true;

        // 힌트가 있었을 경우
        if(hintCheck)
            HintManager.Instance.hintCurrent[hint] = HintManager.Instance.hintMax[hint];
    }
}