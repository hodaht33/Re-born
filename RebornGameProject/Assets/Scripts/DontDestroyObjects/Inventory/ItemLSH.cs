#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 아이템 기능 수행 및 데이터 관리
/// </summary>
public class ItemLSH : MonoBehaviour
{
    // 아이템 이름(스크립트 이름)
    public string ItemName
    {
        get
        {
            return name;
        }
    }

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

    // 아이템 획득 후 이 오브젝트 유지시킬 지 여부
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

    [SerializeField]
    private int mClickCount = 1;

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

    private void OnMouseDown()
    {
        if (mClickCount <= 1)
        {
            StartCoroutine(AddItemCoroutine(1));
        }
        else
        {
            mClickCount--;
        }
    }
    
    private void Update()
    {
        layerMask = 1 << LayerMask.NameToLayer("Tree");
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200.0f, layerMask) == true)
            {
                if (hit.transform.GetComponent<ItemLSH>() == null)
                {
                    return;
                }

                if (hit.transform.GetComponent<ItemLSH>().Equals(this) == true)
                {
                    StartCoroutine(AddItemCoroutine(1));
                }
            }
        }
    }

    private void AddItem()
    {
        // 아이템 획득을 하지 않았으면서 아이템이 획득 되었을 때
        if (IsGetItem == false
            && IsQuestion == false
            && Inventory.Instance.GetItem(this))
        {
            StartCoroutine(Inventory.Instance.UpAndDownInventoryCoroutine());
            gameObject.SetActive(bKeepActive);
            IsGetItem = true;
        }
    }

    private IEnumerator AddItemCoroutine(int i)
    {
        // 아이템 획득을 하지 않았으면서 아이템이 획득 되었을 때
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
                    Renderer[] renderers = GetComponentsInChildren<Renderer>();

                    foreach(Renderer renderer in renderers)
                    {
                        renderer.enabled = false;
                    }
                }
            }

            gameObject.GetComponent<Collider>().enabled = false;
            IsGetItem = true;

            yield return Inventory.Instance.StartAndGetCoroutineUpAndDownInventory();

            gameObject.SetActive(bKeepActive);
        }
    }
}