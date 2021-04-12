using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 컨텐츠 표시 관련 스크립트
/// </summary>
public class Content : SingletonBase<Content>
{
    // 기존에 표시했던 컨텐츠
    private List<GameObject> shownContent;

    private void Start()
    {
        shownContent = new List<GameObject>();
    }

    public void ShowContent(GameObject content, float time)
    {
        // 컨텐츠 중복인지 확인
        foreach (GameObject shown in shownContent)
        {
            if (shown == content)
                return;
        }

        shownContent.Add(content);

        // 컨텐츠 표시
        StartCoroutine(ShowTimer(content, time)); 
    }

    private IEnumerator ShowTimer(GameObject content, float time)
    {
        content.SetActive(true);

        yield return new WaitForSeconds(time);
        content.SetActive(false);
    }
}