using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 게임 시스템 관리
/// </summary>
public class SystemManager : MonoBehaviour
{
    private MouseCursor mouseCursor;
    
    [SerializeField]
    SettingsMenu mSettingsMenu;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            mSettingsMenu.VolumeSliderValue = PlayerPrefs.GetFloat("Volume");
            mSettingsMenu.BrightnessSliderValue = PlayerPrefs.GetFloat("Brightness");
            mSettingsMenu.Resolution = PlayerPrefs.GetString("Resolution");
        }
        mouseCursor = transform.Find("MouseCursor").GetComponent<MouseCursor>();
    }

    private void Update()
    {
        // TODO : 나중에 로딩 시에만 적용되도록 변경
        //if (Input.GetKeyDown(KeyCode.N) == true)
        //{
        //    Loading();
        //}
    }

    public void Loading()
    {
        mouseCursor.ControllSandGlassAnim();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("Volume", mSettingsMenu.VolumeSliderValue);
        PlayerPrefs.SetFloat("Brightness", mSettingsMenu.BrightnessSliderValue);
        PlayerPrefs.SetString("Resolution", mSettingsMenu.Resolution);
    }
}
