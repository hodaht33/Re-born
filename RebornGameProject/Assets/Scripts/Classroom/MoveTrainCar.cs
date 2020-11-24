using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 박서현
/// 기능 : 삭제삭제!
/// </summary>

public class MoveTrainCar : MonoBehaviour
{
    [SerializeField]
    private float mMove = 15f;
    //GameObject parent = gameObject.transform.parent;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.Translate(new Vector3(mMove,0,0));
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
