#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 드랍다운
/// </summary>
public class CustomDropdown : MonoBehaviour
{
    // 현재 선택된 해상도 Text
    [SerializeField]
    private Text mCurrentResolutionText;

    // 해상도 목록을 띄우는 Canvas와 Panel
    [SerializeField]
    private Canvas mResolutionListCanvas;
    private Transform mResolutionListPanel;

    // 해상도 아이템 프리팹
    [SerializeField]
    private CustomDropdownItem mCustomPrefab;

    // 해상도 값들
    [SerializeField]
    private string[] mResolutionList;

    // 해상도 아이템을 담을 List
    private List<CustomDropdownItem> mCustomDropdownItems = new List<CustomDropdownItem>();

    #region 설정창을 닫을 때 해상도 드랍다운 목록이 열려있으면 닫도록하는 이벤트 함수
    public void CloseDropdownList()
    {
        if (mResolutionListCanvas.enabled == true)
        {
            mResolutionListCanvas.enabled = false;
        }
    }
    #endregion

    #region CurrentResolutionPanel클릭 시 이벤트 함수
    // CurrentResolutionPanel의 EventTrigger에 이벤트 추가
    public void MouseDown()
    {
        if (mResolutionListCanvas.enabled == true)
        {
            mResolutionListCanvas.enabled = false;
        }
        else
        {
            mResolutionListCanvas.enabled = true;
        }
    }
    #endregion

    #region 해상도 변경 이벤트 함수
    public void ClickOtherResolution()
    {
        // 선택된 해상도 변경 버튼의 CustomDropdownItem을 EventSystem으로 받아옴
        CustomDropdownItem selectedButton = EventSystem.current.currentSelectedGameObject.GetComponent<CustomDropdownItem>();
        List<string> customDropdownItemsStr = new List<string>();   // 해상도 정렬을 위한 List

        if (selectedButton != null)
        {
            // 현재 적용중인 해상도와 선택된 해상도 문자열 변경
            string tempStr = selectedButton.ItemText;
            selectedButton.ItemText = mCurrentResolutionText.text;
            mCurrentResolutionText.text = tempStr;

            // 나머지 해상도 List에 저장
            for(int i = 0; i < mCustomDropdownItems.Count; ++i)
            {
                customDropdownItemsStr.Add(mCustomDropdownItems[i].ItemText);
            }
            
            // 해상도 문자열을 '*'를 기준으로 잘라 앞 뒤 해상도 값으로 비교하여 내림차순 정렬
            customDropdownItemsStr.Sort
            (
                delegate (string a, string b)
                {
                    // 정렬 시 비교용 리스트
                    // 자른 문자열을 foreach로 받아 넣기위해 List를 이용
                    List<int> nums1 = new List<int>();
                    List<int> nums2 = new List<int>();

                    // 문자열 잘라 List에 값 대입
                    foreach (string str in a.Split('*'))
                    {
                        nums1.Add(int.Parse(str));
                    }

                    foreach(string str in b.Split('*'))
                    {
                        nums2.Add(int.Parse(str));
                    }

                    // 앞 뒤 해상도로 내림차순 정렬
                    if (nums1[0] < nums2[0])
                    {
                        return 1;
                    }
                    else if (nums1[0] > nums2[0])
                    {
                        return -1;
                    }
                    else
                    {
                        // 앞의 해상도가 같을 경우 뒷 해상도로 비교
                        if (nums1[1] < nums2[1])
                        {
                            return 1;
                        }
                        else if (nums1[1] > nums2[1])
                        {
                            return -1;
                        }
                    }

                    return 0;
                }
            );

            // 정렬된 문자열 차례대로 대입
            for(int i = 0; i < mCustomDropdownItems.Count; ++i)
            {
                mCustomDropdownItems[i].ItemText = customDropdownItemsStr[i];
            }
        }
    }
    #endregion

    #region 현재 선택된 해상도 반환 함수
    public string GetSelectedOption()
    {
        return mCurrentResolutionText.text;
    }
    #endregion


    #region 초기화 Awake함수
    private void Awake()
    {
        // 해상도 아이템을 담을 Panel
        mResolutionListPanel = mResolutionListCanvas.transform.GetChild(0).Find("ScrollRect").GetChild(0);

        // 0번째 해상도를 기본값으로 설정
        mCurrentResolutionText.text = mResolutionList[0];

        // 0번째 해상도를 제외한 해상도 아이템 생성
        for (int i = 1; i < mResolutionList.Length; ++i)
        {
            CustomDropdownItem obj = Instantiate(mCustomPrefab, mResolutionListPanel);
            obj.ItemText = mResolutionList[i];

            Vector3 objPos = obj.transform.position;
            objPos.y -= (i - 1) * 30;
            obj.transform.position = objPos;

            obj.Button.onClick.AddListener(ClickOtherResolution);
            obj.Button.onClick.AddListener(FindObjectOfType<SettingsMenu>().ChangeResolution);

            mCustomDropdownItems.Add(obj);
        }

        // Exit버튼 클릭 시 이벤트 추가
        UIManager.Instance.OnClickDropdownExitButton += CloseDropdownList;
    }
    #endregion
}
