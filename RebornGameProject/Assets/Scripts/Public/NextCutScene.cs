using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : EventTrigger를 통해 마우스 버튼 클릭 시 다음 컷씬 장면 불러오기
/// </summary>
public class NextCutScene : MonoBehaviour
{
    public void Next()
    {
        CutSceneManager.Instance.NextCutScene();
    }
}
