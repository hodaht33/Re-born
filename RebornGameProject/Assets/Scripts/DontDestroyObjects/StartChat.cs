using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 대화창 시작 및 내용 관리
/// </summary>
public class StartChat : MonoBehaviour
{
    [SerializeField]
    private string[] texts;
    private int currentClickCount = 0;
    private int currentClickCountImg = 0;
    public Sprite[] LargeImgs;
    public int state = 0; // 0: chat + image, 1: image, 2: chat

    // EventTrigger사용할 경우 사용
    public void Click()
    {
        if (UIManager.Instance.IsActivateSettings == false & state != 2)
        {
            GameObject large = GameObject.Find("Canvas").transform.Find("LargeImg").gameObject;

            if (large.activeSelf == false)
            {
                if (currentClickCountImg < LargeImgs.Length)
                {
                    if (LargeImgs[currentClickCountImg] != null)
                    {
                        large.SetActive(true);
                        large.transform.GetComponent<Image>().sprite = LargeImgs[currentClickCountImg];
                        GameObject.Find("Canvas").transform.Find("back_gray").gameObject.SetActive(true);
                    }
                    ++currentClickCountImg;
                }
                else
                {
                    large.SetActive(true);
                    large.transform.GetComponent<Image>().sprite = LargeImgs[currentClickCountImg - 1];
                    GameObject.Find("Canvas").transform.Find("back_gray").gameObject.SetActive(true);
                }
            }
        }

        if (Chat.Instance.IsActivateChat == false & state != 1)
        {
            if (currentClickCount < texts.Length)
            {
                Chat.Instance.ActivateChat(texts[currentClickCount]);
                ++currentClickCount;
            }
            else
            {
                Chat.Instance.ActivateChat(texts[currentClickCount - 1]);
            }
        }
    }

    // Collider를 넣어 사용 할 수 있는 3D오브젝트가 사용
    public void OnMouseDown()
    {

        if (UIManager.Instance.IsActivateSettings == false & state != 2)
        {
            GameObject large = GameObject.Find("Canvas").transform.Find("LargeImg").gameObject;

            if (large.activeSelf == false)
            {
                if (currentClickCountImg < LargeImgs.Length)
                {
                    if (LargeImgs[currentClickCountImg] != null)
                    {
                        large.SetActive(true);
                        large.transform.GetComponent<Image>().sprite = LargeImgs[currentClickCountImg];
                        GameObject.Find("Canvas").transform.Find("back_gray").gameObject.SetActive(true);
                    }
                    ++currentClickCountImg;
                }
                else
                {
                    large.SetActive(true);
                    large.transform.GetComponent<Image>().sprite = LargeImgs[currentClickCountImg - 1];
                    GameObject.Find("Canvas").transform.Find("back_gray").gameObject.SetActive(true);
                }
            }
        }


        if (Chat.Instance.IsActivateChat == false & state != 1)
        {
            if (currentClickCount < texts.Length)
            {
                Chat.Instance.ActivateChat(texts[currentClickCount]);
                ++currentClickCount;
            }
            else
            {
                Chat.Instance.ActivateChat(texts[currentClickCount - 1]);
            }
        }
    }
}
