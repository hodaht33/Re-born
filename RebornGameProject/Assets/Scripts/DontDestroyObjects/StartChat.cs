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
    [SerializeField]
    private Sprite[] LargeImgs;
    [SerializeField]
    private string[] sounds = null;

    private int currentClickCount = 0;
    private int currentClickCountImg = 0;
    private int currentClickCountSound = 0;

    // EventTrigger사용할 경우 사용
    public void Click()
    {
        if (Chat.Instance.IsActivateChat == false)
        {
            if (currentClickCount >= texts.Length)
            {
                currentClickCount = texts.Length - 1;
            }
            if (currentClickCountImg >= LargeImgs.Length)
            {
                currentClickCountImg = LargeImgs.Length - 1;
            }
            if (currentClickCountSound >= sounds.Length)
            {
                currentClickCountSound = sounds.Length - 1;
            }


            if (texts.Length != 0 && texts[currentClickCount] != "" && LargeImgs[currentClickCount] != null)
            {
                Chat.Instance.ActivateChat(texts[currentClickCount], LargeImgs[currentClickCount]);
            }
            else if (texts.Length != 0 && texts[currentClickCount] != "")
            {
                Chat.Instance.ActivateChat(texts[currentClickCount]);
            }
            else if (LargeImgs[currentClickCount] != null)
            {
                Chat.Instance.ActivateChat(LargeImgs[currentClickCount]);
            }

            if (sounds.Length != 0 && sounds[currentClickCountSound] != "")
            {
                SoundManager.Instance.SetAndPlaySFX(sounds[currentClickCountSound]);
            }


            ++currentClickCount;
            ++currentClickCountImg;
            ++currentClickCountSound;
        }
    }

    // Collider를 넣어 사용 할 수 있는 3D오브젝트가 사용
    public void OnMouseDown()
    {
        if (Chat.Instance.IsActivateChat == false)
        {
            if (currentClickCount >= texts.Length)
            {
                currentClickCount = texts.Length - 1;
            }
            if (currentClickCountImg >= LargeImgs.Length)
            {
                currentClickCountImg = LargeImgs.Length - 1;
            }
            if (currentClickCountSound >= sounds.Length)
            {
                currentClickCountSound = sounds.Length - 1;
            }


            if (texts.Length != 0 && texts[currentClickCount] != "" && LargeImgs[currentClickCount] != null)
            {
                Chat.Instance.ActivateChat(texts[currentClickCount], LargeImgs[currentClickCount]);
            }
            else if (texts.Length != 0 && texts[currentClickCount] != "")
            {
                Chat.Instance.ActivateChat(texts[currentClickCount]);
            }
            else if (LargeImgs[currentClickCount] != null)
            {
                Chat.Instance.ActivateChat(LargeImgs[currentClickCount]);
            }

            if (sounds.Length != 0 && sounds[currentClickCountSound] != "")
            {
                SoundManager.Instance.SetAndPlaySFX(sounds[currentClickCountSound]);
            }


            ++currentClickCount;
            ++currentClickCountImg;
            ++currentClickCountSound;
        }
    }
}