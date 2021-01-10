#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMove mPlayerMove;
    private bool mbMove = true;
    [SerializeField]
    private PlayerRotate mPlayerRotate;
    private bool mbRotate;
    [SerializeField]
    private Light mFlashLight;

    public void ControllMove(bool canMove)
    {
        mbMove = canMove;
    }

    private void Update()
    {
        if (mbMove == true)
        {
            if (Input.GetKey(KeyCode.A)
                || Input.GetKey(KeyCode.LeftArrow))
            {
                mPlayerMove.MoveLeft();
            }

            if (Input.GetKey(KeyCode.D)
                || Input.GetKey(KeyCode.RightArrow))
            {
                mPlayerMove.MoveRight();
            }

            if (Input.GetKeyUp(KeyCode.A)
                || Input.GetKeyUp(KeyCode.LeftArrow)
                || Input.GetKeyUp(KeyCode.D)
                || Input.GetKeyUp(KeyCode.RightArrow))
            {
                mPlayerMove.StopMove();
            }
        }

        // TODO : 손전등 활성화-비활성화 기능 구현 필요, 활성화 시에만 회전 아닐 땐 앞이나 뒤를 보게 구현
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mbRotate = mbRotate == true ? false : true;
            mFlashLight.enabled = mbRotate;
            mPlayerRotate.enabled = mbRotate;
        }
    }
}
