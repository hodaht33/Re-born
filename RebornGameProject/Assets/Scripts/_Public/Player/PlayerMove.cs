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
    private Vector3 mBackVelocity;
    private Vector3 mForwardVelocity;

    [SerializeField]
    private float mSpeed = 3f;

    #region 이동 방식 변경 transform.Translate -> rigidbody.AddForce(이성호)
    public void MoveLeft()
    {
        mRigidbody.velocity = mBackVelocity;
        mRigidbody.AddForce(-transform.forward * Time.deltaTime, ForceMode.Acceleration);
    }

    public void MoveRight()
    {
        mRigidbody.velocity = mForwardVelocity;
        mRigidbody.AddForce(transform.forward * Time.deltaTime, ForceMode.Acceleration);
    }
    #endregion

    public void StopMove()
    {
        mRigidbody.velocity = Vector3.zero;
    }

    private void Awake()
    {
        mRigidbody = GetComponent<Rigidbody>();
        mBackVelocity = -transform.forward * mSpeed;
        mForwardVelocity = transform.forward * mSpeed;
    }
}
