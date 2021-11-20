#pragma warning disable CS0649

using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 아이템 기능 수행 및 데이터 관리
/// </summary>
public class ItemLSH : MonoBehaviour
{
    // 아이템 이름
    public string ItemName
    {
        get
        {
            return name;
        }
    }

    // 관련 힌트 이름
    [SerializeField]
    private PuzzleHint hint;

    // 힌트가 있는지 확인
    [SerializeField]
    private bool hintCheck = false;

    // 스프라이트 이미지
    [SerializeField]
    private Sprite mSprite;
    public Sprite Sprite
    {
        get
        {
            return mSprite;
        }
    }

    // 아이템 획득 후 아이템을 가지고 있던 오브젝트 유지시킬 지 없앨 지 여부
    [SerializeField]
    private bool bKeepActive;

    // 조합이 가능한 아이템 목록
    [SerializeField]
    private string[] mCombineItemNames; // 열쇠조각 같이 2개 이상과 조합 가능한 경우(일반 열쇠조각과 2개가 합쳐진 열쇠조각)
    public string[] CombineItemNames
    {
        get
        {
            return mCombineItemNames;
        }
    }

    // 결과물로 나올 아이템 목록
    [SerializeField]
    private ItemLSH[] mResultItems;
    public ItemLSH[] ResultItems
    {
        get
        {
            return mResultItems;
        }
    }

    // 아이템을 획득했는 지 여부
    private bool mbGetItem;
    public bool IsGetItem
    {
        get
        {
            return mbGetItem;
        }
        set
        {
            mbGetItem = value;
        }
    }

    // 여러번 클릭해야 얻어지는 아이템이 존재
    [SerializeField]
    private int mClickCount = 1;
    private int mClickIndex = 0;
    [SerializeField]
    private SoundInfo.ESfxList[] mClickSFXList;

    [SerializeField]
    private bool bQuestion;
    public bool IsQuestion
    {
        get
        {
            return bQuestion;
        }
        set
        {
            bQuestion = value;
        }
    }

    private int layerMask;

    private void Awake()
    {

        // 열쇠를 가지는 특정 나무에서 열쇠를 얻도록 하기위한 Tree 레이어마스크
        layerMask = 1 << LayerMask.NameToLayer("Tree");
        --mClickCount;
    }

    // 아이템 클릭 이벤트
    private void OnMouseDown()
    {
        // UI 패널이 활성화되어있으면 리턴
        if (UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        if (mClickIndex < mClickCount)
        {
            if (mClickSFXList.Length != 0)
            {
                // 아이템 클릭 효과음 재생
                SoundManager.Instance.SetAndPlaySFX(mClickSFXList[mClickIndex]);
            }
            ++mClickIndex;
        }
        else
        {
            StartCoroutine(AddItemCoroutine());
            if (hintCheck)
                HintManager.Instance.hintCurrent[hint] = HintManager.Instance.hintMax[hint];
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 200.0f, layerMask) == true)
            {
                if (hit.transform.GetComponent<ItemLSH>() == null)
                {
                    return;
                }

                if (hit.transform.GetComponent<ItemLSH>().Equals(this) == true)
                {
                    StartCoroutine(AddItemCoroutine());
                }
            }
        }
    }

    // 아이템 획득 코루틴
    private IEnumerator AddItemCoroutine()
    {
        // 아이템을 가져간 적이 없을 때 아이템이 넘겨지도록 함
        if (IsGetItem == false
            && IsQuestion == false
            && ItemManager.Instance.GetItem(ItemName) != null)
        {
            Inventory.Instance.GetItem(ItemManager.Instance.GetItem(ItemName));
            SoundManager.Instance.SetAndPlaySFX(SoundInfo.ESfxList.GetItem);

            if (bKeepActive == false)
            {
                if (gameObject.GetComponent<Renderer>() != null)
                {
                    gameObject.GetComponent<Renderer>().enabled = false;
                }
                else
                {
                    gameObject.SetActive(false);
                    //Renderer[] renderers = GetComponentsInChildren<Renderer>();

                    //foreach (Renderer renderer in renderers)
                    //{
                    //    renderer.enabled = false;
                    //}
                }
            }

            gameObject.GetComponent<Collider>().enabled = false;
            IsGetItem = true;

            // 아이템 획득 후 인벤토리 위 아래로 보여줌(이를 위해 코루틴으로 만든 메서드)
            yield return Inventory.Instance.StartAndGetCoroutineUpAndDownInventory();

            gameObject.SetActive(bKeepActive);
        }
    }
}