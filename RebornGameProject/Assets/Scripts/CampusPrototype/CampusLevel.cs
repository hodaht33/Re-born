using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 문제 해결 여부에 따른 기능 제한
/// </summary>
public class CampusLevel : LevelManager
{
    [SerializeField] private Light directionalLight;

    public override bool StartLevel()
    {
        directionalLight.gameObject.SetActive(true);
        GetCurrentQuestion().StartQuestion();
        //TODO : 각 레벨 끝을 문제 해결로 하지 않고 플레이어의 이동 위치로 변경

        return true;
    }

    public override bool EndLevel()
    {
        base.EndLevel();
        directionalLight.gameObject.SetActive(false);

        return true;
    }
}
