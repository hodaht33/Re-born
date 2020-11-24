using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 플레이어 이동
/// </summary>

public class PlayerMove : MonoBehaviour
{
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
    
    private void Update()
    {
        if (Input.GetKey (KeyCode.RightArrow) || 
            Input.GetKey (KeyCode.D))
        {
            transform.Translate(Vector3.forward * mSpeed * Time.deltaTime);
        }
        if (Input.GetKey (KeyCode.LeftArrow) || 
            Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * mSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Z) || 
            Input.GetKey(KeyCode.W))
        {
            if (!mbJumping)
            {
                mbJumping = true;
                Jump();
            }
        }
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
