using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 각 레벨 퍼즐 관리 추상 클래스
/// </summary>
public abstract class LevelPuzzle : MonoBehaviour
{
    // 퍼즐별로 가져야 할 데이터들
    [System.Serializable]
    protected struct PuzzleData
    {
        [SerializeField]
        public NextSceneTrigger mNextSceneTrigger;  // 이동될 다음 씬
        [SerializeField, Tooltip("아이템은 씬 내의 DontDestroyObjectInGame내의 ItemPrefabs에서 가져다 사용")]
        public ItemLSH[] mRequiredItems;    // 다음 씬으로 이동하기위해 필요한 아이템 목록
        [SerializeField]
        public Puzzle[] mPuzzles;   // 수행할 퍼즐 목록
    }

    [SerializeField]
    protected PuzzleData mData;

    public ItemLSH[] RequireItems
    {
        get
        {
            return mData.mRequiredItems;
        }
    }

    public Puzzle[] Puzzles
    {
        get
        {
            return mData.mPuzzles;
        }
    }

    protected void Awake()
    {
        // 이벤트 등록
        if (mData.mNextSceneTrigger != null)
        {
            mData.mNextSceneTrigger.OnCheckRequiredItems += CheckRequiredItem;
            mData.mNextSceneTrigger.OnCheckSuccessAllPuzzles += CheckSuccessAllPuzzles;
        }
    }

    public abstract void EndLevel();

    // 필요 아이템 소지 여부 확인 메서드
    private bool CheckRequiredItem()
    {
        for (int i = 0; i < mData.mRequiredItems.Length; ++i)
        {
            if (Inventory.Instance.FindItem(mData.mRequiredItems[i].ItemName) == false)
            {
                return false;
            }
        }

        return true;
    }

    // 모든 퍼즐 수행 여부 확인 메서드
    private bool CheckSuccessAllPuzzles()
    {
        for (int i = 0; i < mData.mPuzzles.Length; ++i)
        {
            if (mData.mPuzzles[i].IsEndPuzzle == false)
            {
                return false;
            }
        }

        return true;
    }
}
