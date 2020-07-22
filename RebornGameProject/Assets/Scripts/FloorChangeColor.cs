using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorChangeColor : MonoBehaviour
{
    public Color color;

    // Start is called before the first frame update
    void Start()
    {
        color = gameObject.GetComponent<Renderer>().material.color;
        gameObject.GetComponent<Renderer>().material.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("OnTriggerEnter: " + color);
            gameObject.GetComponent<Renderer>().material.color = color;
        }

        //other.gameObject.SetActive(false)
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("OnTriggerExit: " + color);
            gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
    }
}
