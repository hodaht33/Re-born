using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 대화창 종료
/// </summary>
public class EndChat : MonoBehaviour
{
    // 대화창 종료 이벤트
    public delegate void EndChatting();
    public event EndChatting endChatEvent;

    public void EndIt()
    {
        endChatEvent();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return)
            || Input.GetKey(KeyCode.Space))
        {
            EndIt();
        }
        else if (Input.GetMouseButtonDown(0))
        {

        }
    }
}
