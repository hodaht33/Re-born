using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 의자 치우기 퍼즐 구현
/// </summary>
public class ChairPuzzle : MonoBehaviour
{
    [SerializeField]
    private PlayerDetect playerDetect;  // Player의 탐지 범위
    [SerializeField]
    private Transform holdTransform;    // Player가 의자를 드는 위치
    [SerializeField]
    private Transform baseTransform;    // 기본 맵 Transform
    
    private bool holdChair; // Player가 의자를 들고 있는지 여부
    private GameObject currentHold; // 현재 들고 있는 의자 오브젝트

    private void Start()
    {
        holdChair = false;
    }

    private void Update()
    {
        // 스페이스를 이용하여 의자 컨트롤
        if(Input.GetKeyUp(KeyCode.Space))
        {
            // 의자를 들고 있지 않은 경우
            if (!holdChair)
            {
                for (int i = 0; i < playerDetect.getDetected().Count; i++)
                {
                    if (playerDetect.getDetected()[i].tag == "Chair")
                    {
                        // 탐지한 의자를 currentHold에 참조 시킴
                        currentHold = playerDetect.getDetected()[i];

                        // 의자를 Player가 들어 올림
                        currentHold.GetComponent<Rigidbody>().isKinematic = true;
                        currentHold.transform.parent = holdTransform;
                        currentHold.transform.localPosition = Vector3.zero;
                        currentHold.transform.localRotation = Quaternion.Euler(0,0,0);
                        holdChair = true;
                        break;
                    }
                }
            }
            // 의자를 놓음
            else
            {
                currentHold.transform.parent = baseTransform;
                currentHold.GetComponent<Rigidbody>().isKinematic = false;
                holdChair = false;
            }
        }
    }
}