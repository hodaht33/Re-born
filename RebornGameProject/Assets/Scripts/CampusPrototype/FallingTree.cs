using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 나무 쓰러뜨리기, 일으키기 - 외적, 내적이용
/// </summary>
public class FallingTree : MonoBehaviour
{
    private Transform tree;
    private Vector3 dir;
    private Vector3 newDir;
    private Transform upDirTransform;
    private Vector3 upDir;
    private FallingTreeSequence treeSequence;
    private Collider collider;

    //private bool isFalling = false;
    //public bool IsFalling
    //{
    //    get { return isFalling; }
    //    private set { }
    //}

    // FallingTreeSequence에서 나무가 차례차례 쓰러지게 하기위해 있는 bool 멤버 변수
    private bool isActivateCoroutine;
    public bool IsActivateCoroutine
    {
        get { return isActivateCoroutine; }
        private set { }
    }

    private void Awake()
    {
        tree = transform.Find("TestTree");
        treeSequence = transform.parent.GetComponent<FallingTreeSequence>();
        collider = tree.GetComponent<Collider>();
        collider.enabled = false;

        Vector3 childPos = transform.Find("Direction").position;
        childPos.y = transform.position.y;
        dir = (childPos - tree.position).normalized;
        newDir = Vector3.Cross(Vector3.up, dir);    // 외적을 통해 dir방향으로의 회전을 위한 방향벡터 구함
        upDirTransform = tree.GetChild(0);
    }

    public IEnumerator Falling(float speed, float acceleration)
    {
        isActivateCoroutine = true;

        upDir = (upDirTransform.position - tree.position).normalized;   // 나무의 윗 방향벡터 계산
        float dot = Vector3.Dot(dir, upDir);    // 사이 각을 알기위한 내적 계산

        float fallingSpeed = speed; // 넘어지는 속도

        // acos * rad2deg로 내적 값에 acos취한 값에 라디안값을 각도로 변환
        while (Mathf.Acos(dot) * Mathf.Rad2Deg > 5.0f)  
        {
            fallingSpeed += acceleration * Time.deltaTime;
            upDir = (upDirTransform.position - tree.position).normalized;
            dot = Vector3.Dot(dir, upDir);
            tree.Rotate(newDir * Time.deltaTime * fallingSpeed);

            yield return null;
        }

        isActivateCoroutine = false;
        collider.enabled = true;
    }

    public IEnumerator RiseUp(float speed, float acceleration)
    {
        collider.enabled = false;
        isActivateCoroutine = true;

        upDir = (upDirTransform.position - tree.position).normalized;
        float dot = Vector3.Dot(dir, upDir);
        float riseUpSpeed = speed;

        while (Mathf.Acos(dot) * Mathf.Rad2Deg < 90.0f)
        {
            riseUpSpeed += acceleration * Time.deltaTime;
            upDir = (upDirTransform.position - tree.position).normalized;
            dot = Vector3.Dot(dir, upDir);
            tree.Rotate(-newDir * Time.deltaTime * riseUpSpeed);    // 반대방향이므로 newDir에 음수 붙임

            yield return null;
        }

        isActivateCoroutine = false;
    }
}
