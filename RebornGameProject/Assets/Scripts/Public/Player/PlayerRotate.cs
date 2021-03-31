using System.Collections;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField, Range(2.0f, 20.0f)]
    private float rotateSpeed = 5.0f;
    private float yAxis;

    private void Update()
    {
        yAxis = Input.GetAxis("Mouse X") * rotateSpeed;
        transform.Rotate(0, yAxis, 0);
    }
    //TODO : 회전 비활성화 시 카메라쪽과 창문쪽으로 고정된 방향벡터를 두고 해당 벡터를 기준으로 왼쪽이면 왼쪽으로 오른쪽이면 오른쪽으로 회전.
    //      단, 비활성화 직후가 아닌 경우엔 오른쪽 이동시엔 오른쪽으로, 왼쪽 이동시엔 왼쪽으로 회전
    private IEnumerator RotateToFront()
    {


        yield return null;
    }

    private IEnumerator RotateToBack()
    {


        yield return null;
    }
}
