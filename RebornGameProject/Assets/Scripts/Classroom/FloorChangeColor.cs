using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChangeColor : MonoBehaviour
{
    public Color color;
    public bool isJumping;
    public bool isChild = false;

    // Start is called before the first frame update
    void Start()
    {
        color = gameObject.GetComponent<Renderer>().material.color;
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("OnTriggerEnter: " + color);
            gameObject.GetComponent<Renderer>().material.color = color;
        }

        //other.gameObject.SetActive(false)
    }

    public void ChildEnter(MoveTrainCar child)
    {
        isChild = true;
    }
    
    public void ChildExit(MoveTrainCar child)
    {
        isChild = false;
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }


    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("OnTriggerExit: " + color);
            isJumping = other.gameObject.GetComponent<PlayerMove>().isJumping;

            if (!isJumping && !isChild)
            {
                gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
        }
    }
}
