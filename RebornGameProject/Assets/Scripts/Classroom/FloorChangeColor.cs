using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 삭제삭제 예정!
/// </summary>

public class FloorChangeColor : MonoBehaviour
{
    [SerializeField]
    private Color mColor;
    [SerializeField]
    private bool mbJumping;
    [SerializeField]
    private bool mbChild = false;

    void Start()
    {
        mColor = gameObject.GetComponent<Renderer>().material.color;
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponent<Renderer>().material.color = mColor;
        }

        //other.gameObject.SetActive(false)
    }

    public void ChildEnter(MoveTrainCar child)
    {
        mbChild = true;
    }
    
    public void ChildExit(MoveTrainCar child)
    {
        mbChild = false;
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }
    
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            mbJumping = other.gameObject.GetComponent<PlayerMove>().CanJumping;

            if (mbJumping == false && 
                mbChild == false)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
        }
    }
}
