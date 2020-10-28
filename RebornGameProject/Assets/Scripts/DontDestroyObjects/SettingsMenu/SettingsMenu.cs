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
    private Slider volumeSlider;
    #region 밝기 조절 멤버
    [SerializeField]
    private Slider brightnessSlider;
    [SerializeField]
    private Image brightnessImage;
    [SerializeField, Range(0, 255)]
    private float minBrightnessValue;
    [SerializeField, Range(0, 255)]
    private float maxBrightnessValue;
    #endregion
    [SerializeField]
    private CustomDropdown resolutionDropdown;
    
    private Color imageColor;

    #region 볼륨 조절 함수
    public void ChangeVolume()
    {
        SoundManager.Instance.MasterVolume = volumeSlider.value;
    }
    #endregion

    #region 밝기 변경 함수
    public void ChangeBrightness()
    {
        // 이 옵션은 오브젝트가 빛을 받는 세기만 바뀌어 화면 자체 밝기 조절은 불가
        // RenderSettings.ambientIntensity = brightnessSlider.value;
        // Debug.Log(RenderSettings.ambientIntensity);
        
        // 맨 앞에 위치한 검은색 이미지의 알파값을 변경해 밝기 조절
        imageColor = brightnessImage.color;
        imageColor.a = 
            Mathf.Clamp(
                (255.0f - brightnessSlider.value * 255.0f) / 255.0f,
                (255.0f - maxBrightnessValue) / 255.0f,
                (255.0f - minBrightnessValue) / 255.0f
            );
        // alpha값을 조절하므로 max가 0에 가깝고 min이 1에 가까움
        brightnessImage.color = imageColor;

        //TODO : 최소 최대 밝기에 따라 게임 실행 시 현재 밝기 조절 필요
    }
    #endregion

    #region 해상도 변경 함수
    public void ChangeResolution()
    {
        string resolution = resolutionDropdown.GetSelectedOption();
        string[] res = resolution.Split('*');
        Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), true);
    }
    #endregion
}
