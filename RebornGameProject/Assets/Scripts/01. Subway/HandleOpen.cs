using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 손잡이를 클릭하면 핸들 퍼즐을 오픈
/// </summary>
public class HandleOpen : MonoBehaviour
{
    [SerializeField] HandlePuzzle puzzle;   // 핸들 퍼즐 캔버스

    private void OnMouseDown()
    {
        // UI 패널이 활성화되어있으면 리턴
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        // 퍼즐 실행
        puzzle.DoPuzzle();
    }
}