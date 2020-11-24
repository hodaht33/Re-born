using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public class CamMoveNextLevel : MonoBehaviour
{
    private Camera camera;
    private Transform nextLevelCamPos;
    private bool canMove;
    private bool canRotate;
    private int level = 1;

    private void Awake()
    {
        camera = GetComponent<Camera>();
    }

    public void MovePos(Transform nextLevelCamPos)
    {
        this.nextLevelCamPos = nextLevelCamPos;
        canMove = true;
        canRotate = true;
        camera.cullingMask = -1 << LayerMask.NameToLayer("Level" + level);
        level += 1;
        camera.cullingMask = 1 << LayerMask.NameToLayer("Level" + level);
    }

    private void LateUpdate()
    {
        if (canMove == true
            && (transform.position.x < nextLevelCamPos.position.x
            || transform.position.y < nextLevelCamPos.position.y
            || transform.position.z < nextLevelCamPos.position.z))
        {
            transform.position = Vector3.Lerp(transform.position, nextLevelCamPos.position, Time.deltaTime * 2.0f);
        }
        else
        {
            canMove = false;
        }

        if (canRotate == true
            )//&& ())
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.RotateTowards(transform.rotation, nextLevelCamPos.rotation, 2.0f), Time.deltaTime * 5.0f);
        }
        else
        {
            canRotate = false;
        }

        if (canMove == false && canRotate == false)
        {
            enabled = false;
        }
    }
}
