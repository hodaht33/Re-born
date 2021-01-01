#pragma warning disable CS0649

using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 나무 순서대로 쓰러뜨리기, 레이캐스팅으로 나무문제 순서 관리
/// </summary>
public class TreeQuestion : Puzzle
{
    private CampusLevel mCampusLevel;

    private PlayerController mPlayerController;

    [SerializeField, Tooltip("적용 순서에 맞게 나무들이 쓰러짐")]
    private FallingTree[] mTrees;

    [SerializeField, Tooltip("열쇠 나무들")]
    private ItemLSH[] mKeyTrees;

    [SerializeField, Tooltip("나무 쓰러지는 기본 속도")]
    private float mSpeed = 30.0f;

    [SerializeField, Tooltip("나무 쓰러지는 가속도")]
    private float mAcceleration = 120.0f;

    private int mLayerMask;  // 나무만 레이캐스팅하기위한 레이어 마스크
    private int mCurrentTreeIndex;
    private Coroutine[] mCoroutines;

    // 모든 나무 일으키기(한번에 일으켜도 상관없으므로 일반 함수 사용, 순서대로 원하면 Coroutine으로 수정 필요)
    public void RiseUpAllTree()
    {
        foreach (FallingTree tree in mTrees)
        {
            StartCoroutine(tree.RiseUpCoroutine(mSpeed, mAcceleration));
        }
    }

    public override void StartPuzzle()
    {
        mPlayerController.ControllMove(false);
        StartCoroutine(FallingAllTreeCoroutine());
    }

    public override void EndPuzzle()
    {
        mPlayerController.ControllMove(true);
        IsEndPuzzle = true;
        mCampusLevel.EndLevel();
    }

    private void Awake()
    {
        mCampusLevel = transform.parent.GetComponent<CampusLevel>();
        mPlayerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        mCoroutines = new Coroutine[mTrees.Length];
        mLayerMask = 1 << LayerMask.NameToLayer("Tree");
    }

    private void Update()
    {
        // 나무 클릭(레이캐스팅 사용, 레이어 마스크로 나무만 클릭되도록 구현)
        if (Input.GetMouseButtonDown(0) == true)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200.0f, mLayerMask) == true)
            {
                // 순서에 맞으면 진행
                if (hit.transform.gameObject.Equals(mTrees[mCurrentTreeIndex].transform.GetChild(0).gameObject) == true)
                {
                    mCoroutines[mCurrentTreeIndex] = StartCoroutine(mTrees[mCurrentTreeIndex].RiseUpCoroutine(mSpeed, mAcceleration));
                    ++mCurrentTreeIndex;

                    if (mCurrentTreeIndex >= mTrees.Length)
                    {
                        EndPuzzle();
                        
                        for (int i = 0; i < mKeyTrees.Length; ++i)
                        {
                            mKeyTrees[i].GetComponent<Collider>().enabled = true;
                            mKeyTrees[i].GetComponent<ItemLSH>().IsQuestion = false;
                        }
                        
                        enabled = false;
                    }
                }
                else
                {
                    StartCoroutine(RiseUpAllTreeCoroutine());

                    mCurrentTreeIndex = 0;
                }
            }
        }
    }

    // 모든 나무 쓰러뜨리기
    private IEnumerator FallingAllTreeCoroutine()
    {
        foreach (FallingTree tree in mTrees)
        {
            StartCoroutine(tree.FallingCoroutine(mSpeed, mAcceleration));
            
            while (tree.IsActivateCoroutine == true)
            {
                yield return null;
            }

            yield return null;
        }

        foreach (FallingTree tree in mTrees)
        {
            tree.Collider.enabled = true;
        }
    }

    private IEnumerator RiseUpAllTreeCoroutine()
    {
        for (int i = 0; i < mTrees.Length - 1; ++i)
        {
            StartCoroutine(mTrees[i].RiseUpCoroutine(mSpeed, mAcceleration));

            yield return null;
        }

        yield return StartCoroutine(mTrees[mTrees.Length - 1].RiseUpCoroutine(mSpeed, mAcceleration));

        StartCoroutine(FallingAllTreeCoroutine());
    }
}
