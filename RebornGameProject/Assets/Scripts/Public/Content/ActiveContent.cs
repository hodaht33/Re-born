using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 클릭시 특정 오브젝트 활성화
/// </summary>
public class ActiveContent : MonoBehaviour
{
    [SerializeField] GameObject[] content;  // 활성화할 오브젝트
    [SerializeField] bool active;           // 활성화 가능여부

    private void OnMouseDown()
    {
        if (!active) return;

        foreach(GameObject temp in content)
            temp.SetActive(true);
    }

    // 활성화 여부 설정
    public void SetActive(bool flag)
    {
        active = flag;
    }
}
