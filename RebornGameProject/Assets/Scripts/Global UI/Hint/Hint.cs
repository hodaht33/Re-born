using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [System.Serializable]
    public struct HintElement
    {
        [SerializeField]
        public string name;
        [SerializeField]
        public string[] script;
    }

    public HintElement[] hintGroup;
}
