using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 해상도 변경 버튼, 텍스트 관리
/// </summary>
public class CustomDropdownItem : MonoBehaviour
{
    // 드랍다운 아이템인 Button프로퍼티
    private Button button;
    public Button Button
    {
        get { return button; }
        private set { }
    }

    // 해상도를 보여주는 버튼 내의 Text프로퍼티
    private Text text;
    public string ItemText
    {
        get { return text.text; }
        set
        {
            text.text = value;
        }
    }

    // 버튼 클릭 이벤트
    public delegate void Click();
    public event Click clickEvent;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<Text>();
    }

    // 클릭 시 이벤트 실행
    public void ClickEvent()
    {
        clickEvent.Invoke();
    }
}
