using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float Speed = 3f;
    public float jumpPower = 5f;
    public bool isJumping = false;
    Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.RightArrow))
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime);
        }
        if (Input.GetKey (KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.back * Speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Z))
        {
            if (!isJumping)
            {
                isJumping = true;
                Jump();
            }
        }
    }

    void Jump()
    {
        if (!isJumping)
            return;

        rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("floor"))
        {
            isJumping = false;
        }
    }
}
