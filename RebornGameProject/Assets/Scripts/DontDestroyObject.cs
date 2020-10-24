using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 씬 변경 후에도 오브젝트 유지
/// </summary>
public class DontDestroyObject : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
