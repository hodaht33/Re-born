#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : Campus씬 문 트리거
/// </summary>
public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform mLeftDoorTransform;
    [SerializeField]
    private Transform mRightDoorTransform;
    [SerializeField]
    private Collider mDoorCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            if (Inventory.Instance.UseItem("Key") == true)
            {
                SoundManager.Instance.SetAndPlaySFX("UseKey");

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

    private IEnumerator OpenCampusDoorCoroutine()
    {
        PlayerMove playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        playerMove.enabled = false;

        yield return new WaitForSeconds(2.0f);

        SoundManager.Instance.SetAndPlaySFX("OpenDoor");

        while (mLeftDoorTransform.eulerAngles.y < 90.0f)
        {
            mLeftDoorTransform.Rotate(Vector3.up, Time.deltaTime * 30.0f);
            mRightDoorTransform.Rotate(Vector3.up, -(Time.deltaTime * 30.0f));

            yield return null;
        }

        mLeftDoorTransform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        mRightDoorTransform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);

        playerMove.enabled = true;
    }
}
