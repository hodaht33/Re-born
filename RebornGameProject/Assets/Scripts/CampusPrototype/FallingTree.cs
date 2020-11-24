using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 나무 쓰러뜨리기, 일으키기 - 외적, 내적이용
/// </summary>
public class FallingTree : MonoBehaviour
{
    private Transform mTree;             // 나무
    private Vector3 mDir;                // 방향벡터
    private Vector3 mNewDir;             // 회전할 방향벡터
    private Transform mUpDirTransform;   // 내적을 위해 방향을 나타낼 오브젝트
    private Vector3 mUpDir;              // 나무의 윗 방향벡터
    private TreeQuestion mTreeSequence;

    // 레이캐스팅을 위한 콜라이더
    private Collider mCollider;
    public Collider Collider
    {
        get
        {
            return mCollider;
        }
        private set
        {
        }
    }

    // FallingTreeSequence에서 나무가 차례차례 쓰러지게 하기위해 있는 bool 멤버 변수
    private bool mbActivateCoroutine;
    public bool IsActivateCoroutine
    {
        get
        {
            return mbActivateCoroutine;
        }
        private set
        {
        }
    }

    // 쓰러뜨리기
    public IEnumerator FallingCoroutine(float speed, float acceleration)
    {
        mbActivateCoroutine = true; // 코루틴 실행

        mUpDir = (mUpDirTransform.position - mTree.position).normalized;   // 나무의 윗 방향벡터 계산

        float dot = Vector3.Dot(mDir, mUpDir);    // 사이 각을 알기위한 내적 계산
        float fallingSpeed = speed; // 넘어지는 속도

        // acos * rad2deg로 내적 값에 acos취한 값(각도)에 라디안값을 각도로 변환
        // 쓰러질 때까지 반복
        while (Mathf.Acos(dot) * Mathf.Rad2Deg > 5.0f)
        {
            fallingSpeed += acceleration * Time.deltaTime;  // 기본 속도에 가속도를 더함
            mUpDir = (mUpDirTransform.position - mTree.position).normalized;   // 계속 내적 계산을 위해 현재 위치의 로컬 윗 방향벡터 계산
            dot = Vector3.Dot(mDir, mUpDir);  // 내적 계산
            mTree.Rotate(mNewDir * Time.deltaTime * fallingSpeed);    // newDir 방향의 회전축으로 fallingSpeed만큼 회전
                                                                    //transform.Rotate(newDir * Time.deltaTime * fallingSpeed);
            yield return null;
        }

        mbActivateCoroutine = false;    // 코루틴 종료
    }

    // 일으키기
    public IEnumerator RiseUpCoroutine(float speed, float acceleration)
    {
        mCollider.enabled = false;   // 일으키면 더이상 누를 필요 없으므로 콜라이더 비활성화
        mbActivateCoroutine = true;

        // 쓰러뜨리기와 newDir에 음수 붙이는 차이 뿐이므로 설명 생략
        mUpDir = (mUpDirTransform.position - mTree.position).normalized;

        float dot = Vector3.Dot(mDir, mUpDir);
        float riseUpSpeed = speed;

        // 일으켜질 때까지 반복
        while (Mathf.Acos(dot) * Mathf.Rad2Deg < 90.0f)
        {
            riseUpSpeed += acceleration * Time.deltaTime;
            mUpDir = (mUpDirTransform.position - mTree.position).normalized;
            dot = Vector3.Dot(mDir, mUpDir);
            mTree.Rotate(-mNewDir * Time.deltaTime * riseUpSpeed);    // 반대방향이므로 newDir에 음수 붙임

            yield return null;
        }

        mbActivateCoroutine = false;
    }

    private void Awake()
    {
        mTree = transform.GetChild(0);   // 실제 나무 오브젝트 가져오기

        mTreeSequence = transform.parent.GetComponent<TreeQuestion>();    // 나무 문제 관리 스크립트 가져오기
        mCollider = mTree.GetComponent<Collider>();   // 나무의 콜라이더 가져오기
        mCollider.enabled = false;   // 초기엔 꺼둬서 막 누르지 못하도록 함

        Vector3 childPos = transform.Find("Direction").position;    // 쓰러질 방향의 위치 가져오기
        childPos.y = mTree.position.y;
        mDir = (childPos - mTree.position).normalized;    // 해당 방향에 대한 방향벡터 구함
        mNewDir = Vector3.Cross(Vector3.up, mDir);    // 외적을 통해 dir방향으로의 회전을 위한 회전축 방향벡터를 구함
        mUpDirTransform = mTree.transform.Find("UpDir");
    }
}
