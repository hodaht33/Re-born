using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 대화창 종료
/// </summary>
public class EndChat : MonoBehaviour
{
    public delegate void EndChatting();
    public event EndChatting endChatEvent;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Return)
            || Input.GetKey(KeyCode.Space))
        {
            EndIt();
        }
    }

    public void EndIt()
    {
        endChatEvent.Invoke();
    }
}
