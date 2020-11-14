using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveNextLevel : MonoBehaviour
{
    private Transform nextLevelCamPos;
    private bool canMove;

    public void MovePos(Transform nextLevelCamPos)
    {
        this.nextLevelCamPos = nextLevelCamPos;
        canMove = true;
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
    }
}
