using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 각 레벨별 퍼즐 관리 기능을 위한 추상 클래스
/// </summary>
public class LevelPuzzle : MonoBehaviour
{   
    [System.Serializable]
    protected struct PuzzleData
    {
        [SerializeField]
        public NextSceneTrigger mNextSceneTrigger;
        [SerializeField]
        public ItemLSH[] mRequiredItems;
        [SerializeField]
        public Puzzle[] mPuzzles;
    }

    protected void EndLevel(ref PuzzleData data)
    {
        data.mNextSceneTrigger.enabled = true;
    }
}
