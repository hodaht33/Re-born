using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] private Transform leftDoorTransform;
    [SerializeField] private Transform rightDoorTransform;
    [SerializeField] private Collider doorCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            if (Inventory.Instance.UseItem("Key") == true)
            {
                SoundManager.Instance.SetAndPlaySFX("UseKey");

                StartCoroutine(OpenCampusDoor());
                doorCollider.enabled = false;
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                Chat.Instance.ActivateChat("문이 잠겨있다.", null, true);
            }
        }
    }

    private IEnumerator OpenCampusDoor()
    {
        PlayerMove playerMove = GameObject.FindWithTag("Player").GetComponent<PlayerMove>();
        playerMove.enabled = false;

        yield return new WaitForSeconds(2.0f);

        SoundManager.Instance.SetAndPlaySFX("OpenDoor");

        while (leftDoorTransform.eulerAngles.y < 90.0f)
        {
            leftDoorTransform.Rotate(Vector3.up, Time.deltaTime * 30.0f);
            rightDoorTransform.Rotate(Vector3.up, -(Time.deltaTime * 30.0f));

            yield return null;
        }

        leftDoorTransform.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
        rightDoorTransform.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);

        playerMove.enabled = true;
    }
}
