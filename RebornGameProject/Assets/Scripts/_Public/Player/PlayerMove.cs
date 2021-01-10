using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 플레이어 이동
/// </summary>

public class PlayerMove : MonoBehaviour
{
    private Rigidbody mRigidbody;
    private Vector3 mLeftVelocity;
    private Vector3 mRightVelocity;

    [SerializeField]
    private float mSpeed = 3f;
    [SerializeField]
    private float mJumpPower = 5f;
    [SerializeField]
    private bool mbJumping = false;
    public bool CanJumping
    {
        get
        {
            return mbJumping;
        }
        private set
        {

        }
    }

    #region 이동 부분 함수로 만들어 외부(PlayerController.cs)에서 호출하도록 수정, 점프 삭제(이성호)
    //private void Update()
    //{
    //    if (Input.GetKey(KeyCode.RightArrow) ||
    //        Input.GetKey(KeyCode.D))
    //    {
    //        transform.Translate(Vector3.forward * mSpeed * Time.deltaTime);
    //    }

    //    if (Input.GetKey(KeyCode.LeftArrow) ||
    //        Input.GetKey(KeyCode.A))
    //    {
    //        transform.Translate(Vector3.back * mSpeed * Time.deltaTime);
    //    }

    //    if (Input.GetKey(KeyCode.Z) ||
    //        Input.GetKey(KeyCode.W))
    //    {
    //        if (!mbJumping)
    //        {
    //            mbJumping = true;
    //            Jump();
    //        }
    //    }
    //}

    public void MoveRight()
    {
        //transform.Translate(Vector3.right* mSpeed * Time.deltaTime);
        mRigidbody.velocity = mRightVelocity;
        mRigidbody.AddForce(Vector3.right * Time.deltaTime, ForceMode.Acceleration);
    }

    public void MoveLeft()
    {
        //transform.Translate(Vector3.left * mSpeed * Time.deltaTime);
        mRigidbody.velocity = mLeftVelocity;
        mRigidbody.AddForce(Vector3.left * Time.deltaTime, ForceMode.Acceleration);
    }
    #endregion

    public void StopMove()
    {
        mRigidbody.velocity = Vector3.zero;
    }

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mLeftVelocity  = new Vector3(-mSpeed, .0f, .0f);
        mRightVelocity = new Vector3(mSpeed, .0f, .0f);
}

    private void Update()
    {
        Debug.Log(mRigidbody.velocity);
    }

    private void Jump()
    {
        if (mbJumping == false)
        {
            return;
        }

        GetComponent<Rigidbody>().AddForce(Vector3.up * mJumpPower, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("floor") == true)
        {
            mbJumping = false;
        }
    }
}
