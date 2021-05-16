using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 3D 오브젝트 클릭 관련 기능
/// </summary>
public class MousePoint : SingletonBase<MousePoint>
{
    public delegate void PointObject(Transform obj);
    public PointObject pointObject;

    public Camera mainCamera;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButton(0))  // 마우스가 클릭 되면
        {
            RaycastHit hit = new RaycastHit();
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out hit))
            {
                if(pointObject != null)
                    pointObject(hit.transform);
            }
        }
    }
}