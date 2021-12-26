using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PuzzleHint
{
    subwayChair,
    subwayBoard,
    subwayBox,
    subwayCobweb,
    campusTree,
    campusKey
}

public class Hint : MonoBehaviour
{
    [System.Serializable]
    public struct HintElement
    {
        public PuzzleHint puzzle;
        public string[] script;
    }

    public HintElement[] hintGroup;
}