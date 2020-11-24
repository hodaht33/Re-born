using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public class CamLookPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private void Update()
    {
        transform.LookAt(player);
    }
}
