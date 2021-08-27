using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 플레이어 바라보기
/// </summary>
public class LookPlayer : MonoBehaviour
{
    private Transform mPlayer;

    private void Awake()
    {
        mPlayer = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        transform.LookAt(mPlayer);
    }
}
