using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeChatImg : MonoBehaviour
{
    [SerializeField]
    private GameObject other;
    [SerializeField]
    private Sprite[] LargeImgs;

    public void OnMouseDown()
    {
        other.GetComponent<StartChat>().setLargeImgs(LargeImgs);
    }
}
