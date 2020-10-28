using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 나무 쓰러뜨리기, 일으키기
/// </summary>
public class FallingTree : MonoBehaviour
{
    // TODO : 현재는 하나만 쓰러뜨리지만 이후엔 차례대로 쓰러뜨리도록 모든 나무를 한번에 관리해야 함
    // TODO : 나무 레이캐스팅 시 마스크를 통해 플레이어는 눌리지 않도록 해야 정상 작동 예상
    private Vector3 dir;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float fallRiseSpeed = 20.0f;

    private void Awake()
    {
        Vector3 childPos = transform.parent.Find("Direction").position;
        childPos.y = transform.position.y;
        //dir = (childPos - transform.position).normalized;
        transform.LookAt(childPos);
    }

    private Coroutine curr = null;
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (curr != null)
            {
                StopCoroutine(curr);
            }
            curr = StartCoroutine(Falling());
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (curr != null)
            {
                StopCoroutine(curr);
            }
            curr = StartCoroutine(RiseUp());
        }
    }

    private IEnumerator Falling()
    {
        float fallingSpeed = speed;
        float prevAngleX = 0.0f;
        while (transform.eulerAngles.x - prevAngleX >= 0.0f)
        {
            prevAngleX = transform.eulerAngles.x;
            fallingSpeed += Time.deltaTime * fallRiseSpeed;
            transform.Rotate(Vector3.right * Time.deltaTime * fallingSpeed);

            yield return null;
        }

        transform.eulerAngles = new Vector3(90.0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    private IEnumerator RiseUp()
    {
        float riseUpSpeed = speed;

        while (180.0f - transform.eulerAngles.x > 0.0f)
        {
            riseUpSpeed += Time.deltaTime * fallRiseSpeed;
            transform.Rotate(Vector3.left * Time.deltaTime * riseUpSpeed);

            yield return null;
        }

        transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, transform.eulerAngles.z);
    }
}
