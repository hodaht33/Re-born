using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 퍼즐 해결을 기록하고 매니저에게 반영
/// </summary>
public abstract class Puzzle : MonoBehaviour
{
    // 
    public bool IsEndPuzzle
    {
        get;
        set;
    }

    // 게임 시작 시 퍼즐 엔딩은 초기화
    private void Start()
    {
        IsEndPuzzle = false;
    }

    // 퍼즐이 끝났음을 반영
    public void SetPuzzleEnd()
    {
        IsEndPuzzle = true;
    }
}