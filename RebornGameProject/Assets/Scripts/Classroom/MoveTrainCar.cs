using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrainCar : MonoBehaviour
{
    [SerializeField]
    private float move = 15f;
    //GameObject parent = gameObject.transform.parent;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.Translate(new Vector3(move,0,0));
            transform.parent.GetComponent<FloorChangeColor>().ChildEnter(this);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<FloorChangeColor>().ChildExit(this);
        }
    }
}
