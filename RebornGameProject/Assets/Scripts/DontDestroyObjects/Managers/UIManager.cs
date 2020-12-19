#pragma warning disable CS0649

using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 전체 UI 활성화 관리
/// </summary>
public class UIManager : SingletonBase<UIManager>
{
    [SerializeField]
    private Canvas mSettingsCanvas;  // 설정창 캔버스
    [SerializeField]
    private Canvas mMenuCanvas;      // 메뉴창 캔버스

    // 현재 활성화중인 캔버스
    private Canvas mCurrentEnableCanvas;

    // 닫기 버튼 이벤트
    public delegate void ClickExitButton();
    //public event ClickExitButton OnClickExitButton;
    public event ClickExitButton OnClickDropdownExitButton;

    private bool mbIsActivateSettings = false;
    public bool IsActivateSettings
    {
        get
        {
            return mbIsActivateSettings;
        }
        private set
        {

        }
    }

    #region 설정창 열기 이벤트 함수
    // SettingsActivateButton 이벤트에 적용
    public void OpenSettings()
    {
        mCurrentEnableCanvas = mSettingsCanvas;
        mSettingsCanvas.enabled = true;
        mMenuCanvas.enabled = false;

        //TODO : 효과음 잘라야 함
        //SoundManager.instance.SetAndPlaySFX("UI");
    }
    #endregion

    #region 활성화된 UI 닫기 버튼 이벤트 함수
    // ExitButton 이벤트에 적용
    public void ExitMenu()
    {
        if (mCurrentEnableCanvas.Equals(mSettingsCanvas))
        {
            OnClickDropdownExitButton.Invoke();
        }
        
        mCurrentEnableCanvas.enabled = false;
        mCurrentEnableCanvas = null;
        mMenuCanvas.enabled = true;
    }
    #endregion


    private void Awake()
    {
        if (instance != null &&
            instance != this)
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
        if (Input.GetKeyDown(KeyCode.Escape) &&
            mCurrentEnableCanvas == null)
        {
            mbIsActivateSettings = true;
            OpenSettings();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) &&
            mCurrentEnableCanvas != null)
        {
            ExitMenu();
            mbIsActivateSettings = false;
        }
    }

}
