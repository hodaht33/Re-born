using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 힌트 관련 기능
/// </summary>
public class HintManager : MonoBehaviour
{
    [SerializeField]
    private GameObject hintPanel;   // 힌트 표시 패널

    [SerializeField]
    private Text hintText;          // 힌트 설명

    [SerializeField]
    private Image hintGage;         // 힌트 게이지

    // 게이지를 소모하고 힌트 표시
    public void OpenDescription(string description)
    {
        if (hintGage.fillAmount < 0.2f)
            return;

        hintGage.fillAmount -= 0.2f;
        hintPanel.SetActive(true);
        hintText.text = description;
    }
}