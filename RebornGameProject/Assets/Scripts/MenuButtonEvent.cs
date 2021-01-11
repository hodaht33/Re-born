using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class MenuButtonEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent OnClickOptionButtonEvent;

    public void OnClickStartButton()
    {
        CutSceneManager.Instance.PlayCutScene(SceneInfoManager.EScene.Subway);
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
}
