using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 단서 관련 컨텐츠를 표시하는 스크립트
/// </summary>
public class ShowContent : MonoBehaviour
{
    [System.Serializable]
    private struct Condition    // 컨텐츠 표시 전 조건 확인 (인스펙터에서 무조건 인덱스가 적을 수록 체크 오브젝트 많아야 함.)
    {
        public GameObject[] check;    // 관련 오브젝트 존재 체크
        public GameObject content;    // 표시 컨텐츠
    }

    [SerializeField] Condition[] conditions;
    [SerializeField] float showTime;            // 표시 시간

    private void OnMouseDown()
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            bool possible = true;
            foreach (GameObject temp in conditions[i].check)
            {
                if (!temp.activeInHierarchy)
                {
                    possible = false;
                    break;
                }
            }

            if (possible)
            {
                Content.Instance.ShowContent(conditions[i].content, showTime);
                return;
            }
        }
    }
}