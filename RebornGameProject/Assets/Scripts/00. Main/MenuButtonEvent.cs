using System.Collections;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 작성자 : 이성호
/// 기능 : MainMenu의 각 버튼 이벤트 관리
/// </summary>
public class MenuButtonEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnClickOptionButtonEvent;
    [SerializeField]
    private Canvas mMenuCanvas;
    private bool mStart;

    public void OnClickStartButton()
    {
        if (mStart == false)
        {
            mStart = true;
            CutSceneManager.Instance.PlayCutScene(SceneInfoManager.EScene.Subway);
            StartCoroutine(Delay());
        }
    }

    public void OnClickOptionButton()
    {
        OnClickOptionButtonEvent.Invoke();
    }

    public void OnClickExitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);

        mMenuCanvas.enabled = true;
    }
}
