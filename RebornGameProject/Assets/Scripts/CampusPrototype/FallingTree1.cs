using System.Collections;
using UnityEngine;

/// <summary>
/// 삭제 예정
/// 
/// 작성자 : 이성호
/// 기능 : 나무 쓰러뜨리기, 일으키기 - 외적, 내적이용
/// </summary>
public class FallingTree1 : MonoBehaviour
{
    private Transform tree;             // 나무
    private Vector3 dir;                // 방향벡터
    private Vector3 newDir;             // 회전할 방향벡터
    private Transform upDirTransform;   // 내적을 위해 방향을 나타낼 오브젝트
    private Vector3 upDir;              // 나무의 윗 방향벡터
    private TreeQuestion treeSequence;

    // 레이캐스팅을 위한 콜라이더
    private Collider collider;
    public Collider Collider
    {
        get { return collider; }
        private set { }
    }
    
    // FallingTreeSequence에서 나무가 차례차례 쓰러지게 하기위해 있는 bool 멤버 변수
    private bool isActivateCoroutine;
    public bool IsActivateCoroutine
    {
        get { return isActivateCoroutine; }
        private set { }
    }

    private void Awake()
    {
        tree = transform.GetChild(0);
        collider = tree.GetComponent<Collider>();   // 나무의 콜라이더 가져오기
        collider.enabled = false;   // 초기엔 꺼둬서 막 누르지 못하도록 함

        Vector3 childPos = transform.Find("Direction").position;    // 쓰러질 방향의 위치 가져오기
        childPos.y = transform.position.y;  // y좌표 값은 현재 오브젝트와 같도록 적용
        //childPos.y = tree.position.y;
        //dir = (childPos - tree.position).normalized;    // 해당 방향에 대한 방향벡터 구함
        dir = (childPos - transform.position).normalized;
        newDir = Vector3.Cross(Vector3.up, dir);    // 외적을 통해 dir방향으로의 회전을 위한 회전축 방향벡터를 구함
                                                    //upDirTransform = tree.GetChild(0);  // 내적 계산을 위해 나무의 로컬 윗 방향벡터를 가져옴
        upDirTransform = transform.Find("UpDir");
    }

    private void OnDrawGizmos()
    {
        Vector3 childPos = transform.Find("Direction").position;    // 쓰러질 방향의 위치 가져오기
        childPos.y = transform.position.y;  // y좌표 값은 현재 오브젝트와 같도록 적용
        //childPos.y = tree.position.y;
        //dir = (childPos - tree.position).normalized;    // 해당 방향에 대한 방향벡터 구함
        dir = (childPos - transform.position).normalized;
        newDir = Vector3.Cross(Vector3.up, dir);

        Gizmos.DrawLine(transform.position, dir * 100.0f);
        //Gizmos.DrawLine(transform.position, newDir * 100.0f);
    }

    //private void Awake()
    //{
    //    tree = transform.GetChild(0);   // 실제 나무 오브젝트 가져오기

    //    treeSequence = transform.parent.GetComponent<TreeQuestion>();    // 나무 문제 관리 스크립트 가져오기
    //    collider = tree.GetComponent<Collider>();   // 나무의 콜라이더 가져오기
    //    collider.enabled = false;   // 초기엔 꺼둬서 막 누르지 못하도록 함

    //    Vector3 childPos = transform.Find("Direction").position;    // 쓰러질 방향의 위치 가져오기
    //    childPos.y = transform.position.y;  // y좌표 값은 현재 오브젝트와 같도록 적용
    //    //childPos.y = tree.position.y;
    //    //dir = (childPos - tree.position).normalized;    // 해당 방향에 대한 방향벡터 구함
    //    dir = (childPos - transform.position).normalized;
    //    newDir = Vector3.Cross(Vector3.up, dir);    // 외적을 통해 dir방향으로의 회전을 위한 회전축 방향벡터를 구함
    //    //upDirTransform = tree.GetChild(0);  // 내적 계산을 위해 나무의 로컬 윗 방향벡터를 가져옴
    //    upDirTransform = transform.Find("UpDir");
    //}

    //// 쓰러뜨리기
    //public IEnumerator Falling(float speed, float acceleration)
    //{
    //    isActivateCoroutine = true; // 코루틴 실행

    //    upDir = (upDirTransform.position - tree.position).normalized;   // 나무의 윗 방향벡터 계산
    //    //upDir = (upDirTransform.position - transform.position).normalized;
    //    float dot = Vector3.Dot(dir, upDir);    // 사이 각을 알기위한 내적 계산
    //    float fallingSpeed = speed; // 넘어지는 속도

    //    // acos * rad2deg로 내적 값에 acos취한 값(각도)에 라디안값을 각도로 변환
    //    // 쓰러질 때까지 반복
    //    while (Mathf.Acos(dot) * Mathf.Rad2Deg > 5.0f)  
    //    {
    //        fallingSpeed += acceleration * Time.deltaTime;  // 기본 속도에 가속도를 더함
    //        upDir = (upDirTransform.position - tree.position).normalized;   // 계속 내적 계산을 위해 현재 위치의 로컬 윗 방향벡터 계산
    //        //upDir = (upDirTransform.position - transform.position).normalized;
    //        dot = Vector3.Dot(dir, upDir);  // 내적 계산
    //        tree.Rotate(newDir * Time.deltaTime * fallingSpeed);    // newDir 방향의 회전축으로 fallingSpeed만큼 회전
    //        //transform.Rotate(newDir * Time.deltaTime * fallingSpeed);

    //        yield return null;
    //    }

    //    isActivateCoroutine = false;    // 코루틴 종료
    //    //collider.enabled = true;
    //}

    //// 일으키기
    //public IEnumerator RiseUp(float speed, float acceleration)
    //{
    //    collider.enabled = false;   // 일으키면 더이상 누를 필요 없으므로 콜라이더 비활성화
    //    isActivateCoroutine = true;

    //    // 쓰러뜨리기와 newDir에 음수 붙이는 차이 뿐이므로 설명 생략
    //    upDir = (upDirTransform.position - tree.position).normalized;
    //    //upDir = (upDirTransform.position - transform.position).normalized;
    //    float dot = Vector3.Dot(dir, upDir);
    //    float riseUpSpeed = speed;

    //    // 일으켜질 때까지 반복
    //    while (Mathf.Acos(dot) * Mathf.Rad2Deg < 90.0f)
    //    {
    //        riseUpSpeed += acceleration * Time.deltaTime;
    //        upDir = (upDirTransform.position - tree.position).normalized;
    //        //upDir = (upDirTransform.position - transform.position).normalized;
    //        dot = Vector3.Dot(dir, upDir);
    //        tree.Rotate(-newDir * Time.deltaTime * riseUpSpeed);    // 반대방향이므로 newDir에 음수 붙임
    //        //transform.Rotate(-newDir * Time.deltaTime * riseUpSpeed);

    //        yield return null;
    //    }

    //    isActivateCoroutine = false;
    //}
}
