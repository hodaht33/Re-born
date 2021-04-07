using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField]
    private PlayerDetect playerDetect;  // Player의 탐지 범위
    [SerializeField]
    private Transform holdTransform;    // Player가 의자를 드는 위치
    [SerializeField]
    private Transform baseTransform;    // 기본 맵 Transform

    private bool holdObject; // Player가 물건을 들고 있는지 여부
    private GameObject currentHold; // 현재 들고 있는 오브젝트

    private void Start()
    {
        holdObject = false;
    }

    private void Update()
    {
        // 스페이스를 이용하여 물건 컨트롤
        if (Input.GetKeyUp(KeyCode.Space))
        {
            // 물건을 들고 있지 않은 경우
            if (!holdObject)
            {
                for (int i = 0; i < playerDetect.getDetected().Count; i++)
                {
                    // 무슨 물건을 들고 있는가에 따라 행동이 다름
                    switch(playerDetect.getDetected()[i].tag)
                    {
                        case "Chair":
                            GrabObject(playerDetect.getDetected()[i]);
                            return;
                    }
                }
            }
            // 물건을 놓을 때
            else
            {
                switch(currentHold.tag)
                {
                    // 의자 상태에 따른 상태 설정
                    case "Chair":
                        ChairPuzzle chairPuzzle = currentHold.GetComponent<ChairPuzzle>();
                        if (chairPuzzle.GetInArea())
                            chairPuzzle.SetFinalState();
                        else
                            chairPuzzle.SetFirstState();

                        holdObject = false;
                        currentHold.transform.parent = baseTransform;
                        return;
                }
            }
        }
    }

    private void GrabObject(GameObject obj)
    {
        // 탐지한 물건을 currentHold에 참조 시킴
        currentHold = obj;

        // 의자를 Player가 들어 올림
        currentHold.transform.parent = holdTransform;
        currentHold.transform.localPosition = Vector3.zero;
        currentHold.transform.localRotation = Quaternion.Euler(0, 0, 0);
        holdObject = true;
    }
}
