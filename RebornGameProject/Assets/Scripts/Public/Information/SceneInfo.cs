using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 각각의 씬 정보 관리
/// </summary>
[CreateAssetMenu(menuName = "Info/SceneInfo")]
public class SceneInfo : ScriptableObject
{
    private string mSceneName;
    public string SceneName
    {
        get
        {
            return mSceneName;
        }

        private set { }
    }

    [SerializeField]
    private int mSceneIndex;
    public int SceneIndex
    {
        get
        {
            return mSceneIndex;
        }

        private set { }
    }

    [SerializeField]
    private bool mHaveCutscene;
    public bool HaveCutscene
    {
        get
        {
            return mHaveCutscene;
        }

        private set { }
    }

    public void Awake()
    {
        mSceneName = name;
    }
}
