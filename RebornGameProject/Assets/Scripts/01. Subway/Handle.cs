using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 권준호
/// 기능 : 손잡이 위 아래 이동, 이벤트 발생
/// </summary>
public class Handle : MonoBehaviour
{
    public HandlePuzzle Parent;
    public int Index;

    private new RectTransform transform;
    private Vector3 defaultPoistion;
    private Vector3 destPosition;
    private bool isPulled = false;

    private void Start()
    {
        transform = GetComponent<RectTransform>();
        defaultPoistion = transform.position;
        destPosition = defaultPoistion;

        Button button = gameObject.AddComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    public void Release()
    {
        if (!isPulled) return;
        destPosition = defaultPoistion;
        isPulled = false;
        enabled = true;
    }

    public void OnClicked()
    {
        if (isPulled) return;
        destPosition = defaultPoistion + new Vector3(0, -40, 0);
        isPulled = true;
        enabled = true;
    }

    private void Update()
    {
        Vector3 pos = transform.position;
        if ((pos - destPosition).magnitude > 1f)
        {
            transform.position = Vector3.Lerp(pos, destPosition, Time.deltaTime * 4);
        }
        else
        {
            enabled = false;
            if (isPulled) Parent.OnHandlePulled(Index);
        }
    }
}
