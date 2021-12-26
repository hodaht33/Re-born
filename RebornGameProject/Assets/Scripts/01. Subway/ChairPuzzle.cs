using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 의자 치우기 퍼즐 구현
/// </summary>
public class ChairPuzzle : MonoBehaviour
{
    // 의자가 영역 안에 있는지 확인
    private bool inArea;
    public bool GetInArea()
    {
        return inArea;
    }

    // 의자가 놓였는지 확인
    private bool end = false;
    public bool GetEnd()
    {
        return end;
    }

    private Vector3 firstPosition;  // 의자의 처음 position
    private Quaternion firstRotation;  // 의자의 처음 rotation

    // 의자를 내려놓았을 때 세팅할 rotation
    [SerializeField]
    private Vector3 finalRotation;

    // 의자 위치 목록
    [SerializeField]
    private ChairPositionGroup positionGroup;

    private void Start()
    {
        // 초기 상태 설정
        inArea = false;
        firstPosition = transform.position;
        firstRotation = transform.rotation;
    }

    // 의자를 잘못 놓았을 때 상태 설정
    public void SetFirstState()
    {
        transform.position = firstPosition;
        transform.rotation = firstRotation;
    }

    // 의자를 성공적으로 내려놓았을 때 상태 설정
    public void SetFinalState()
    {
        SetPosition();
        SetRotation();
        end = true;
    }

    // 의자가 바닥을 보도록 rotation 설정
    public void SetRotation()
    {
        transform.rotation = Quaternion.Euler(finalRotation);
    }

    // 의자가 벽 쪽에 붙을 position 설정
    public void SetPosition()
    {
        transform.position = positionGroup.GetNearest(transform).position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ChairArea"))
            inArea = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ChairArea"))
            inArea = false;
    }
}