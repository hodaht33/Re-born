using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookPlayer : MonoBehaviour
{
    private Transform mPlayer;

    private void Awake()
    {
        mPlayer = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(mPlayer);
    }
}
