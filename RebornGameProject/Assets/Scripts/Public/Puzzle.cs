using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 각 퍼즐별 공통 기능 추상 클래스
/// </summary>
public abstract class Puzzle : MonoBehaviour
{
    // 퍼즐 문제의 종료 여부
    public bool IsEndPuzzle
    {
        get;
        set;
    }

    // 현재로서 사전 준비가 필요한 퍼즐은 없어 확실한 용도는 없지만 일단 둠
    public abstract void StartPuzzle();

    // 실패 시 초기화 또는 아이템 획득이나 퍼즐 완료 시 할 수 있는 동작에 대해 수행 가능하도록 변경하는 용도의 함수
    public abstract void EndPuzzle();
}
