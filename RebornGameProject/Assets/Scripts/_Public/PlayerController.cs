using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PlayerMove playerMove;
    [SerializeField]
    private PlayerRotate playerRotate;
    private bool mCanRotate;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            mCanRotate = mCanRotate == true ? false : true;
            playerRotate.enabled = mCanRotate;
        }
    }
}
