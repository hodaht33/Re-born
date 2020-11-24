using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 설정 창 이벤트 관리
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    [SerializeField]
    private Slider mVolumeSlider;
    #region 밝기 조절 멤버
    [SerializeField]
    private Slider mBrightnessSlider;
    [SerializeField]
    private Image mBrightnessImage;
    [SerializeField, Range(0, 255)]
    private float mMinBrightnessValue;
    [SerializeField, Range(0, 255)]
    private float mMaxBrightnessValue;
    #endregion
    [SerializeField]
    private CustomDropdown mResolutionDropdown;
    
    private Color mImageColor;

    #region 볼륨 조절 함수
    public void ChangeVolume()
    {
        SoundManager.Instance.MasterVolume = mVolumeSlider.value;
    }
    #endregion

    #region 밝기 변경 함수
    public void ChangeBrightness()
    {
        // 이 옵션은 오브젝트가 빛을 받는 세기만 바뀌어 화면 자체 밝기 조절은 불가
        // RenderSettings.ambientIntensity = brightnessSlider.value;
        
        // 맨 앞에 위치한 검은색 이미지의 알파값을 변경해 밝기 조절
        mImageColor = mBrightnessImage.color;
        mImageColor.a = 
            Mathf.Clamp(
                (255.0f - mBrightnessSlider.value * 255.0f) / 255.0f,
                (255.0f - mMaxBrightnessValue) / 255.0f,
                (255.0f - mMinBrightnessValue) / 255.0f
            );
        // alpha값을 조절하므로 max가 0에 가깝고 min이 1에 가까움
        mBrightnessImage.color = mImageColor;

        //TODO : 최소 최대 밝기에 따라 게임 실행 시 현재 밝기 조절 필요
    }
    #endregion

    #region 해상도 변경 함수
    public void ChangeResolution()
    {
        string resolution = mResolutionDropdown.GetSelectedOption();
        string[] res = resolution.Split('*');
        Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), true);
    }
    #endregion
}
