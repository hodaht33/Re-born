#pragma warning disable CS0649

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
    public float VolumeSliderValue  // 볼륨 값 프로퍼티
    {
        get
        {
            return mVolumeSlider.value;
        }

        set
        {
            mVolumeSlider.value = value;
            ChangeVolume();
        }
    }

    [SerializeField]
    private Slider mBrightnessSlider;
    public float BrightnessSliderValue  // 밝기 값 프로퍼티
    {
        get
        {
            return mBrightnessSlider.value;
        }

        set
        {
            mBrightnessSlider.value = value;
            ChangeBrightness();
        }
    }
    [SerializeField]
    private Image mBrightnessImage;
    [SerializeField, Range(0, 255)]
    private float mMinBrightnessValue;
    [SerializeField, Range(0, 255)]
    private float mMaxBrightnessValue;

    [SerializeField]
    private CustomDropdown mResolutionDropdown;
    public string Resolution    // 해상도 프로퍼티
    {
        get
        {
            return mResolutionDropdown.CurrentResolutionText;
        }

        set
        {
            mResolutionDropdown.CurrentResolutionText = value;
            ChangeResolution();
        }
    }

    private Color mImageColor;

    // 볼륨 변경 메서드
    public void ChangeVolume()
    {
        SoundManager.Instance.MasterVolume = mVolumeSlider.value;
    }

    // 밝기 변경 메서드
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
    }

    // 해상도 변경 메서드
    public void ChangeResolution()
    {
        string resolution = mResolutionDropdown.CurrentResolutionText;
        string[] res = resolution.Split('*');
        Screen.SetResolution(int.Parse(res[0]), int.Parse(res[1]), true);
    }
}
