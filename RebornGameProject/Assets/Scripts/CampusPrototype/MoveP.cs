using UnityEngine;

/// <summary>
/// 삭제 예정
/// </summary>
public class MoveP : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
