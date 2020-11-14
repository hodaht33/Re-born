using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 작성자 : 이성호
/// 기능 : 나무 쓰러뜨리기, 일으키기 - LookAt이용
/// </summary>
public class FallingTreeNo : MonoBehaviour
{
    // TODO : 현재는 하나만 쓰러뜨리지만 이후엔 차례대로 쓰러뜨리도록 모든 나무를 한번에 관리해야 함
    // TODO : 나무 레이캐스팅 시 마스크를 통해 플레이어는 눌리지 않도록 해야 정상 작동 예상
    private FallingTreeSequence treeSequence;
    private Collider collider;
    private Vector3 dir;
    
    private bool isFalling = false;
    public bool IsFalling
    {
        get { return isFalling; }
        private set { }
    }

    private bool isActivateCoroutine = false;
    public bool IsActivateCoroutine
    {
        get { return isActivateCoroutine; }
        private set { }
    }

    private void Awake()
    {
        treeSequence = transform.parent.parent.GetComponent<FallingTreeSequence>();
        collider = GetComponent<Collider>();
        collider.enabled = false;

        Vector3 direction = transform.parent.Find("Direction").position;
        direction.y = transform.position.y;
        transform.LookAt(direction);
    }
    
    public IEnumerator Falling(float speed, float acceleration)
    {
        isActivateCoroutine = true;

        float fallingSpeed = speed;
        float prevAngleX = 0.0f;
        while (transform.eulerAngles.x - prevAngleX >= 0.0f)
        {
            prevAngleX = transform.eulerAngles.x;
            fallingSpeed += Time.deltaTime * acceleration;
            transform.Rotate(Vector3.right * Time.deltaTime * fallingSpeed);

            Debug.Log(transform.eulerAngles);
            yield return null;
        }

        transform.eulerAngles = new Vector3(90.0f, transform.eulerAngles.y, transform.eulerAngles.z);

        isFalling = true;
        isActivateCoroutine = false;
        collider.enabled = true;
    }

    public IEnumerator RiseUp(float speed, float acceleration)
    {
        collider.enabled = false;
        isActivateCoroutine = true;

        float riseUpSpeed = speed;

        while (180.0f - transform.eulerAngles.x > 0.0f)
        {
            riseUpSpeed += Time.deltaTime * acceleration;
            transform.Rotate(Vector3.left * Time.deltaTime * riseUpSpeed);

            Debug.Log(transform.eulerAngles);
            yield return null;
        }

        transform.eulerAngles = new Vector3(0.0f, transform.eulerAngles.y, transform.eulerAngles.z);

        isActivateCoroutine = false;
    }
}
