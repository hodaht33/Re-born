#pragma warning disable CS0649

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 컷씬 실행 관리
/// </summary>
public class CutSceneManager : SingletonBase<CutSceneManager>
{
    private Canvas mCanvas;
    private Image mImage;
    private int mCurrentCutSceneIndex;
    private int mSpriteIndex;
    [SerializeField]
    private DissolveImage mDissolveImage;

    // 한 챕터 컷씬 이미지들을 가지는 구조체
    [System.Serializable]
    private struct CutScene
    {
        [SerializeField]
        public Sprite[] sprites;
        //[SerializeField]
        //public SceneInfo.EScene nextScene;
    }

    private SceneInfo.EScene mNextScene;

    // 모든 컷씬 에디터로 관리
    [SerializeField]
    private CutScene[] mCutScenes;
    private Coroutine mNextCoroutine;

    public void PlayCutScene(SceneInfo.EScene nextScene)
    {
        mNextScene = nextScene;
        mCanvas.enabled = true;
        mImage.raycastTarget = true;
        mSpriteIndex = 0;
        mImage.sprite = mCutScenes[mCurrentCutSceneIndex].sprites[mSpriteIndex];
    }

    public void NextCutScene()
    {
        ++mSpriteIndex;

        if (mNextCoroutine == null)
        {
            if (mSpriteIndex >= mCutScenes[mCurrentCutSceneIndex].sprites.Length)
            {
                mDissolveImage.StartDissolve();

                mNextCoroutine = StartCoroutine(EndCutSceneCoroutine());

                return;
            }

            mImage.sprite = mCutScenes[mCurrentCutSceneIndex].sprites[mSpriteIndex];
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        mCanvas = GetComponent<Canvas>();
        mImage = transform.Find("SceneImage").GetComponent<Image>();
    }

    private IEnumerator EndCutSceneCoroutine()
    {
        yield return mNextCoroutine = FadeManager.instance.StartAndGetCoroutineFadeOutOrNull();

        SetEnable(false);
        mImage.raycastTarget = false;

        mNextCoroutine = null;

        SceneManager.LoadScene(SceneInfo.GetSceneName(mNextScene));
    }

    private void SetEnable(bool bEnable)
    {
        mCanvas.enabled = bEnable;
        mDissolveImage.SetDefault();
    }
}
