using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairPositionGroup : MonoBehaviour
{
    // 해당 위치에 의자가 있는지 확인
    private List<bool> chairExist;

    // 의자 위치 목록
    [SerializeField]
    private Transform[] chairPosition;

    // 최대 거리
    [SerializeField]
    private float maxDistance;
    
    private void Start()
    {
        chairExist = new List<bool>();
        for (int i = 0; i < chairPosition.Length; i++)
            chairExist.Add(false);
    }

    // 의자에 가장 가까운 위치 반환
    public Transform GetNearest(Transform target)
    {
        int index = 0;
        float distance = maxDistance;

        // 가장 가까운 위치 확인
        for(int i = 0; i < chairPosition.Length; i++)
        {
            if (chairExist[i]) continue;

            if (Mathf.Abs(target.position.z - chairPosition[i].position.z) < distance)
            { 
                index = i;
                distance = Mathf.Abs(target.position.z - chairPosition[i].position.z);
            }
        }

        // 해당 위치에 존재한다고 표시
        chairExist[index] = true;

        return chairPosition[index];
    }
}