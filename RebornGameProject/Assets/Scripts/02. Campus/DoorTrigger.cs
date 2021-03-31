#pragma warning disable CS0649

using System.Collections;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : Campus씬 문 트리거
/// </summary>
public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform mLeftDoorTransform;   // 문을 정면으로 바라봤을 때 왼쪽 문
    [SerializeField]
    private Transform mRightDoorTransform;  // // 문을 정면으로 바라봤을 때 오른쪽 문
    [SerializeField]
    private Collider mDoorCollider;
    [SerializeField]
    private PlayerController mPlayerController;

    // 트리거 충돌 이벤트
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            // 열쇠를 소지하고 있어서 사용될 때
            if (Inventory.Instance.UseItem("Key") == true)
            {
                SoundManager.Instance.SetAndPlaySFX(SoundInfo.ESfxList.UseKey);
                StartCoroutine(OpenCampusDoorCoroutine());
                mDoorCollider.enabled = false;
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                Chat.Instance.ActivateChat("문이 잠겨있다.", null, true);
            }
        }
    }

    // 문이 천천히 열리도록 하는 코루틴
    private IEnumerator OpenCampusDoorCoroutine()
    {
        mPlayerController.ControllMove(false);

        yield return new WaitForSeconds(2.0f);  // 열쇠 사용 사운드 재생까지 잠깐의 지연시간 부여

        SoundManager.Instance.SetAndPlaySFX(SoundInfo.ESfxList.OpenDoor);

        while (mLeftDoorTransform.eulerAngles.y < 90.0f)
        {
            mLeftDoorTransform.Rotate(Vector3.up, Time.deltaTime * 30.0f);
            mRightDoorTransform.Rotate(Vector3.up, -(Time.deltaTime * 30.0f));

            yield return null;
        }

        mLeftDoorTransform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        mRightDoorTransform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);

        mPlayerController.ControllMove(true);
    }
}
