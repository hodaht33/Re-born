using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 작성자 : 곽진성
/// 기능 : 플레이어의 오브젝트 탐지 구현
/// </summary>
public class PlayerDetect : MonoBehaviour
{
    [SerializeField]
    private string[] validTag;  // 저장이 필요한 태그 목록

    private List<GameObject> detectedObject; // Player에게 탐지된 오브젝트들을 저장
    public List<GameObject> getDetected()
    {
        return detectedObject;
    }

    private void Start()
    {
        detectedObject = new List<GameObject>();
    }

    // Player의 탐지 범위에 들어왔으면 태그 검사 후 저장
    private void OnTriggerEnter(Collider other)
    {
        
        foreach(string valid in validTag)
        {
            if (other.tag == valid)
            {
                detectedObject.Add(other.gameObject);
                break;
            }
        }    
    }

    // Player의 탐지 범위를 벗어났으면 목록에서 제거
    private void OnTriggerExit(Collider other)
    {
        foreach (string valid in validTag)
        {
            if (other.tag == valid)
            {
                detectedObject.Remove(other.gameObject);
                break;
            }
        }
    }
}
