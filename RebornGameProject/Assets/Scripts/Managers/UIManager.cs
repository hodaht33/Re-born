using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 전체 UI 활성화 관리
/// </summary>
public class UIManager : SingletonBase<UIManager>
{
    [SerializeField]
    private Canvas settingsCanvas;  // 설정창 캔버스
    [SerializeField]
    private Canvas menuCanvas;      // 메뉴창 캔버스

    // 현재 활성화중인 캔버스
    private Canvas currentEnableCanvas;

    // 닫기 버튼 이벤트
    public delegate void ClickExitButton();
    //public event ClickExitButton OnClickExitButton;
    public event ClickExitButton OnClickDropdownExitButton;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentEnableCanvas == null)
        {
            OpenSettings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && currentEnableCanvas != null)
        {
            ExitMenu();
        }
    }

    #region 설정창 열기 이벤트 함수
    // SettingsActivateButton 이벤트에 적용
    public void OpenSettings()
    {
        currentEnableCanvas = settingsCanvas;
        settingsCanvas.enabled = true;
        menuCanvas.enabled = false;
    }
    #endregion

    #region 활성화된 UI 닫기 버튼 이벤트 함수
    // ExitButton 이벤트에 적용
    public void ExitMenu()
    {
        //UnityEngine.EventSystems.EventSystem.
        //    current.currentSelectedGameObject.transform.
        //    parent.gameObject.SetActive(false);

        if (currentEnableCanvas.Equals(settingsCanvas))
        {
            OnClickDropdownExitButton.Invoke();
        }

        //OnClickExitButton.Invoke();

        currentEnableCanvas.enabled = false;
        currentEnableCanvas = null;
        menuCanvas.enabled = true;
    }
    #endregion
}
