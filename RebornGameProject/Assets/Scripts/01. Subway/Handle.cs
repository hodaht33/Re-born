using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 권준호
/// 손잡이 위 아래 이동, 이벤트 발생
/// 손잡이가 클릭될 경우 스스로 내려가고, 내려가는 동작이 완료될 경우 OnHandlePulled 이벤트 발생.
/// Release 함수 호출 시 손잡이가 다시 위로 올라가며 이때는 아무런 이벤트도 발생하지 않음.
/// </summary>
public class Handle : MonoBehaviour
{
    public HandlePuzzle Parent;
    public int Index;

    private Vector3 defaultPoistion;    // The default (not pulled) position of handle.
    private Vector3 destPosition;       // The position where handle shoulde be.
    private bool isPulled = false;

    private void Start()
    {
        defaultPoistion = transform.position;
        destPosition = defaultPoistion;
        gameObject.AddComponent<Button>().onClick.AddListener(Pull);
    }

    public void Pull()
    {
        if (isPulled) return;
        isPulled = true;
        enabled = true;
        destPosition = defaultPoistion + new Vector3(0, -40, 0);
    }

    public void Release()
    {
        if (!isPulled) return;
        isPulled = false;
        enabled = true;
        destPosition = defaultPoistion;
    }

    private void Update()
    {
        // Note tha tihs function is called only when this script is enabled.

        Vector3 pos = transform.position;
        if ((pos - destPosition).magnitude > 1f)
        {
            // If current position is not close enough to destination, move closer to destination position.
            transform.position = Vector3.Lerp(pos, destPosition, Time.deltaTime * 4);
        }
        else
        {
            // If current position is close enough to destination, stop this script and invoke pulled event. 
            enabled = false;
            if (isPulled) Parent.OnHandlePulled(Index);
        }
    }
}
