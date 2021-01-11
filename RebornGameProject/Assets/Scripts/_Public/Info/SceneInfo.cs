using UnityEngine;

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

    private void Awake()
    {
        mSceneName = name;
    }
}
