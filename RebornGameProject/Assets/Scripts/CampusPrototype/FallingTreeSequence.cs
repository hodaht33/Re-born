using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 나무 순서대로 쓰러뜨리기, 레이캐스팅으로 나무문제 순서 관리
/// </summary>
public class FallingTreeSequence : MonoBehaviour
{
    [SerializeField, Tooltip("적용 순서에 맞게 나무들이 쓰러짐")]
    private FallingTree[] trees;

    [SerializeField, Tooltip("나무 쓰러지는 기본 속도")]
    private float speed = 30.0f;
    [SerializeField, Tooltip("나무 쓰러지는 가속도")]
    private float acceleration = 120.0f;

    private int currentTreeIndex = 0;
    private Coroutine[] coroutines;
    private int layerMask;  // 나무만 레이캐스팅하기위한 레이어 마스크

    private void Awake()
    {
        //trees = GetComponentsInChildren<FallingTree2>();
        coroutines = new Coroutine[trees.Length];
        layerMask = 1 << LayerMask.NameToLayer("Tree");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            currentTreeIndex = 0;
            StartCoroutine(FallingAllTree());
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            RiseUpAllTree();
        }

        // 나무 클릭(레이캐스팅 사용, 레이어 마스크로 나무만 클릭되도록 구현)
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200.0f, layerMask))
            {
                // 순서에 맞으면 진행
                if (hit.transform.gameObject.Equals(trees[currentTreeIndex].transform.GetChild(0).gameObject))
                {
                    coroutines[currentTreeIndex] = StartCoroutine(trees[currentTreeIndex].RiseUp(speed, acceleration));
                    ++currentTreeIndex;

                    if (currentTreeIndex >= trees.Length)
                    {
                        TreeQuestionSystem.Instance.SuccessQuestion();

                        // 열쇠 조각 획득을 위한 코드
                        for (int i = 0; i < trees.Length; ++i)
                        {
                            trees[i].Collider.enabled = true;
                            trees[i].GetComponent<FallingTree>().enabled = false;
                            if (trees[i].transform.GetChild(0).GetComponent<ItemLSH>() != null)
                            {
                                trees[i].transform.GetChild(0).GetComponent<ItemLSH>().ActiveGetItem = true;
                            }
                        }

                        enabled = false;
                    }
                }
                else
                {
                    // 틀린 순서면 이전에 쓰러뜨린 나무 모두 일으킴
                    //for (int i = 0; i < currentTreeIndex; ++i)
                    //{
                    //    StopCoroutine(coroutines[i]);
                    //    coroutines[i] = null;
                    //    StartCoroutine(trees[i].Falling(speed, acceleration));
                    //}
                    StartCoroutine(RiseUpAllTreeCoroutine());

                    currentTreeIndex = 0;
                }
            }
        }
    }

    // 모든 나무 쓰러뜨리기
    private IEnumerator FallingAllTree()
    {
        foreach(FallingTree tree in trees)
        {
            StartCoroutine(tree.Falling(speed, acceleration));
            while (tree.IsActivateCoroutine == true)
            {
                yield return null;
            }

            yield return null;
        }

        foreach(FallingTree tree in trees)
        {
            tree.Collider.enabled = true;
        }
    }

    // 모든 나무 일으키기(한번에 일으켜도 상관없으므로 일반 함수 사용, 순서대로 원하면 Coroutine으로 수정 필요)
    public void RiseUpAllTree()
    {
        foreach(FallingTree tree in trees)
        {
            StartCoroutine(tree.RiseUp(speed, acceleration));
        }
    }

    private IEnumerator RiseUpAllTreeCoroutine()
    {
        for (int i = 0; i < trees.Length - 1; ++i)
        {
            StartCoroutine(trees[i].RiseUp(speed, acceleration));

            yield return null;
        }

        yield return StartCoroutine(trees[trees.Length - 1].RiseUp(speed, acceleration));

        StartCoroutine(FallingAllTree());
    }
}
