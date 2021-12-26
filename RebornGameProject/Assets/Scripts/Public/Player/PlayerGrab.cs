using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 물체 들기 및 내려놓기
/// </summary>
public class PlayerGrab : MonoBehaviour
{
    [SerializeField]
    private PlayerDetect playerDetect;  // Player의 탐지 범위
    [SerializeField]
    private Transform holdTransform;    // Player가 의자를 드는 위치
    [SerializeField]
    private Transform baseTransform;    // 기본 맵 Transform

    private PlayerController controller;    // Player 컨트롤
    private bool holdObject;                // Player가 물건을 들고 있는지 여부
    private GameObject currentHold;         // 현재 들고 있는 오브젝트
    private bool canAnimate = true;         // 애니메이션 가능 여부

    private void Start()
    {
        controller = gameObject.GetComponent<PlayerController>();
        holdObject = false;
    }

    private void Update()
    {
        // 스페이스를 이용하여 물건 컨트롤
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (!canAnimate) return;

            // 물건을 들고 있지 않은 경우
            if (!holdObject)
            {
                for (int i = 0; i < playerDetect.getDetected().Count; i++)
                {
                    // 무슨 물건을 들고 있는가에 따라 행동이 다름
                    switch(playerDetect.getDetected()[i].tag)
                    {
                        case "Chair":
                            // 의자가 영역 안에 있는지 확인
                            ChairPuzzle chairPuzzle = playerDetect.getDetected()[i].GetComponent<ChairPuzzle>();
                            if (chairPuzzle.GetEnd()) continue;

                            // 플레이어 조종 제한
                            controller.ControllMove(false);
                            controller.ControllRotate(false);
                            canAnimate = false;

                            StartCoroutine(GrabObject(chairPuzzle.gameObject));
                            return;
                    }
                }
            }
            // 물건을 놓을 때
            else
            {
                canAnimate = false;

                switch(currentHold.tag)
                {
                    case "Chair":
                        // 플레이어 조종 제한
                        controller.ControllMove(false);
                        controller.ControllRotate(false);
                        canAnimate = false;

                        StartCoroutine(ReleaseChair());
                        return;
                }
            }
        }
    }

    private IEnumerator GrabObject(GameObject obj)
    {
        // 탐지한 물건을 currentHold에 참조 시킴
        currentHold = obj;

        // 드는 애니메이션 실행
        controller.animator.SetTrigger("raise");

        yield return new WaitForSeconds(1f);

        // 의자를 Player가 들어 올림
        currentHold.transform.parent = holdTransform;
        currentHold.transform.localPosition = Vector3.zero;
        currentHold.transform.localRotation = Quaternion.Euler(0, 0, 0);
        holdObject = true;

        controller.ControllRotate(true);
        canAnimate = true;
    }

    // 의자 내려놓기
    private IEnumerator ReleaseChair()
    {
        // 내리는 애니메이션 실행
        controller.animator.SetTrigger("release");

        yield return new WaitForSeconds(1.5f);

        ChairPuzzle chairPuzzle = currentHold.GetComponent<ChairPuzzle>();
        if (chairPuzzle.GetInArea())
            chairPuzzle.SetFinalState();
        else
            chairPuzzle.SetFirstState();

        holdObject = false;
        currentHold.transform.parent = baseTransform;

        controller.ControllMove(true);
        controller.ControllRotate(true);
        canAnimate = true;
    }
}
