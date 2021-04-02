using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 오브젝트의 낙하
/// </summary>
public class GravityFall : MonoBehaviour
{
    [SerializeField]
    private string floorTag;    // 바닥으로 인식하는 tag
    private new Rigidbody rigidbody;    // 현재 오브젝트의 rigidbody

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 바닥에 닿으면 kinematic으로 변경
        if(collision.transform.CompareTag(floorTag))
        {
            rigidbody.isKinematic = true;
        }
    }
}