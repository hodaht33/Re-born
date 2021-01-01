using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 각 레벨별 퍼즐 관리 공통 기능 추상 클래스
/// </summary>
public abstract class LevelPuzzle : MonoBehaviour
{
    [System.Serializable]
    protected struct PuzzleData
    {
        [SerializeField]
        public NextSceneTrigger mNextSceneTrigger;
        [SerializeField, Tooltip("아이템은 씬 내의 DontDestroyObjectInGame내의 ItemPrefabs에서 가져다 사용")]
        public ItemLSH[] mRequiredItems;
        [SerializeField]
        public Puzzle[] mPuzzles;
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
        if (mData.mNextSceneTrigger != null)
        {
            mData.mNextSceneTrigger.OnCheckRequiredItems += CheckRequiredItem;
            mData.mNextSceneTrigger.OnCheckSuccessAllPuzzles += CheckSuccessAllPuzzles;
        }
    }

    public abstract void EndLevel();

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
