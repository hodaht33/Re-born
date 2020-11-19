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
            if (texts.Length > 0 && texts[currentClickCount] != "" && LargeImgs.Length > 0 && LargeImgs[currentClickCountImg] != null)
            {
                Chat.Instance.ActivateChat(texts[currentClickCount], LargeImgs[currentClickCountImg]);
            }
            else if (texts.Length > 0 && texts[currentClickCount] != "")
            {
                Chat.Instance.ActivateChat(texts[currentClickCount]);
            }
            else if (LargeImgs.Length > 0 && LargeImgs[currentClickCountImg] != null)
            {
                Chat.Instance.ActivateChat(LargeImgs[currentClickCountImg]);
            }

            if (sounds.Length > 0 && sounds[currentClickCountSound] != "")
            {
                SoundManager.Instance.SetAndPlaySFX(sounds[currentClickCountSound]);
            }


            if (currentClickCount < texts.Length - 1)
            {
                currentClickCount++;
            }
            if (currentClickCountImg < LargeImgs.Length - 1)
            {
                currentClickCountImg++;
            }
            if (currentClickCountSound < sounds.Length - 1)
            {
                currentClickCountSound++;
            }
        }
    }

    // Collider를 넣어 사용 할 수 있는 3D오브젝트가 사용
    public void OnMouseDown()
    {
        if (Chat.Instance.IsActivateChat == false)
        {
            if (texts.Length > 0 && texts[currentClickCount] != "" && LargeImgs.Length > 0 && LargeImgs[currentClickCountImg] != null)
            {
                Chat.Instance.ActivateChat(texts[currentClickCount], LargeImgs[currentClickCountImg]);
            }
            else if (texts.Length > 0 && texts[currentClickCount] != "")
            {
                Chat.Instance.ActivateChat(texts[currentClickCount]);
            }
            else if (LargeImgs.Length > 0 && LargeImgs[currentClickCountImg] != null)
            {
                Chat.Instance.ActivateChat(LargeImgs[currentClickCountImg]);
            }

            if (sounds.Length > 0 && sounds[currentClickCountSound] != "")
            {
                SoundManager.Instance.SetAndPlaySFX(sounds[currentClickCountSound]);
            }


            if (currentClickCount < texts.Length - 1)
            {
                currentClickCount++;
            }
            if (currentClickCountImg < LargeImgs.Length - 1)
            {
                currentClickCountImg++;
            }
            if (currentClickCountSound < sounds.Length - 1)
            {
                currentClickCountSound++;
            }
        }
    }
}