using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float Speed = 3f;
    [SerializeField] private float jumpPower = 5f;
    [SerializeField] private bool isJumping = false;
    public bool Jumping { get { return isJumping; } private set { } }
    
    private void Update()
    {
        if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D))
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
        if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W))
        {
            if (!isJumping)
            {
                isJumping = true;
                Jump();
            }
        }
    }

    private void Jump()
    {
        if (isJumping == false)
        {
            return;
        }

        GetComponent<Rigidbody>().AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("floor") == true)
        {
            isJumping = false;
        }
    }
}
