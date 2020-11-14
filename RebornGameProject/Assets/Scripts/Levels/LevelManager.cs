using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Level[] levels;
    private Level currentLevel;

    private void Awake()
    {
        levels = GetComponentsInChildren<Level>();
        currentLevel = levels[0];
        //for (int i = 0; i < levels.Length; ++i)
        //{
        //    //if (levels[i].Equals(gameObject))
        //    //{
        //    //    continue;
        //    //}
        //}
    }

    public void MoveNextLevel()
    {
        // TODO : 다음 레벨로 넘어가려면 각 레벨의 모든 문제를 해결해야 넘어갈 수 있도록 변경(각 문제들은 Level스크립트에서 수행)
        currentLevel.MoveNextLevel();
        currentLevel = currentLevel.NextLevel;
    }
}
