using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 문제 클래스들에 상속시킬 추상 클래스
/// </summary>
public abstract class Question : MonoBehaviour
{
    // 문제 풀이 여부
    protected bool mbSolve;
    public bool IsSolve
    {
        get { return mbSolve; }
        private set { }
    }

    // 각 문제별 풀이방법
    // 사실 필요 없을 수 있지만 다른 조건도 같이 필요한 경우를 생각하여 준비
    protected abstract void Solve();
}
