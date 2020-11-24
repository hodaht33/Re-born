using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 작성자 : 박서현
/// 기능 : (삭제 예정)
/// </summary>


public class LargeImage : MonoBehaviour
{
    //[SerializeField]
    //private Sprite LargeImg;
    public Sprite LargeImg;

    private void OnMouseDown()
    {
        if (UIManager.Instance.IsActivateSettings == false)
        {
            GameObject large = GameObject.Find("Canvas").transform.Find("LargeImg").gameObject;

            if (large.activeSelf == false)
            {
                large.SetActive(true);
                large.transform.GetComponent<Image>().sprite = LargeImg;
                GameObject.Find("Canvas").transform.Find("back_gray").gameObject.SetActive(true);
            }
        }
    }
}
