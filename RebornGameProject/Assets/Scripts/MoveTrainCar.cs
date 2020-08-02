using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrainCar : MonoBehaviour
{
    public float move = 15f;
    //GameObject parent = gameObject.transform.parent;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.Translate(new Vector3(move,0,0));
            //parent.setColor();
        }
    }
}
