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
    [SerializeField]
    private Slider brightnessSlider;
    [SerializeField]
    private Image brightnessImage;
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
        imageColor.a = (150.0f - brightnessSlider.value * 150.0f) / 255.0f;
        brightnessImage.color = imageColor;
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
