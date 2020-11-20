using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubwayStage : MonoBehaviour
{
    [SerializeField] private Collider wallCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true)
        {
            if (Inventory.Instance.FindItem("Phone") == true)
            {
                Debug.Log("Success");
                //TODO: 문 열리며 플레이어 다음으로(캠퍼스로) 이동하면 캠퍼스로 카메라 이동 및 캠퍼스 활성화
                wallCollider.enabled = false;
                Camera.main.GetComponent<SubwayCameraMove>().enabled = false;
            }
            else
            {
                Debug.Log("Fail");
                //TODO: Chat에 뭔가 필요하다고하여 핸드폰을 얻고오도록 유도
            }
        }
    }
}
