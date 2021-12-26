#pragma warning disable CS0649

using UnityEngine;

public enum direction
{
    forward, back, left, right
}

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 플레이어의 컨트롤 담당
/// </summary>
public class PlayerController : MonoBehaviour
{
    // 이동, 회전 제어
    [SerializeField] bool move = true;
    [SerializeField] bool rotate = true;
    
    [SerializeField] float speed = 3f;

    [HideInInspector] public Animator animator;

    // 플레이어의 상하좌우 방향
    private Vector3 forward;
    private Vector3 back;
    private Vector3 left;
    private Vector3 right;

    private bool mbRotate;

    private void Awake()
    {
        // 플레이어 방향 설정
        forward = transform.forward;
        back = -transform.forward;
        left = -transform.right;
        right = transform.right;
    }

    // 방향에 따른 플레이어의 움직임
    public void Move(direction current)
    {
        switch(current)
        {
            case direction.forward:
                transform.Translate(forward * speed);
                break;

            case direction.back:
                transform.Translate(back * speed);
                break;

            case direction.left:
                transform.Translate(left * speed);
                break;

            case direction.right:
                transform.Translate(right * speed);
                break;

            default:
                break;
        }

        animator.SetBool("walk", true);
    }

    public void StopMove()
    {
        animator.SetBool("walk", false);
    }

    public void ControllMove(bool canMove)
    {
        if (canMove == false)
        {
            StopMove();
        }

        move = canMove;
    }

    public void ControllRotate(bool canRotate)
    {
        rotate = canRotate;
    }

    private void Update()
    {
        if (move == true)
        {
            if (Input.GetKey(KeyCode.A)
                || Input.GetKey(KeyCode.LeftArrow))
            {
                Move(direction.left);
            }

            if (Input.GetKey(KeyCode.D)
                || Input.GetKey(KeyCode.RightArrow))
            {
                Move(direction.right);
            }

            if (Input.GetKey(KeyCode.W)
                || Input.GetKey(KeyCode.UpArrow))
            {
                Move(direction.forward);
            }

            if (Input.GetKey(KeyCode.S)
                || Input.GetKey(KeyCode.DownArrow))
            {
                Move(direction.back);
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) ||
                Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) ||
                Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow) ||
                Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                StopMove();
            }
        }
    }

    private void LateUpdate()
    {
        if (!rotate) return;

        // 플레이어가 마우스 방향을 바라보도록 함
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.RaycastAll(ray.origin, ray.direction).Length > 0)
        {
            foreach(RaycastHit hit in Physics.RaycastAll(ray.origin, ray.direction))
            {
                if (hit.transform.tag == "Mouse")
                    transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            }
        }
    }
}