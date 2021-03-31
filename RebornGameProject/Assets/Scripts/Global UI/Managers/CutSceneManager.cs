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
    private Canvas mCanvas; // 컷씬 이미지를 보여줄 캔버스
    private Image mImage;   // 컷씬 이미지
    private int mCurrentCutSceneIndex = -1; // 컷씬 인덱스
    private int mSpriteIndex;   // 컷씬 내의 스프라이트 이미지 인덱스
    [SerializeField]
    private DissolveImage mDissolveImage;   // dissolve셰이더 적용할 이미지

    // 한 컷씬 내의 스프라이트 이미지들을 가지는 구조체
    [System.Serializable]
    private struct CutScene
    {
        [SerializeField]
        public Sprite[] sprites;
        //[SerializeField]
        //public SceneInfo.EScene nextScene;
    }

    private SceneInfoManager.EScene mNextScene; // 컷씬 종료 후 이동알 씬

    // 모든 컷씬 에디터로 관리
    [SerializeField]
    private CutScene[] mCutScenes;
    private Coroutine mNextCoroutine;

    // 컷씬 재생 시작 메서드
    public void PlayCutScene(SceneInfoManager.EScene nextScene)
    {
        mNextScene = nextScene;

        ++mCurrentCutSceneIndex;
        StartCoroutine(StartCutSceneCoroutine());
    }

    // 다음 스프라이트 이미지 보여주는 메서드
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

    // 컷씬 시작 전 페이드 아웃-인을 재생시키는 코루틴
    public IEnumerator StartCutSceneCoroutine()
    {
        yield return FadeManager.Instance.StartAndGetCoroutineFadeOut();
        
        mCanvas.enabled = true;
        mImage.raycastTarget = true;
        mSpriteIndex = 0;
        mImage.sprite = mCutScenes[mCurrentCutSceneIndex].sprites[mSpriteIndex];

        yield return FadeManager.Instance.StartAndGetCoroutineFadeIn();

        enabled = false;
    }

    private void Awake()
    {
        if (instance != null &&
            instance != this)
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
    
    // 컷씬 종료 시 페이드 아웃 후 지정해둔 다음 씬으로 이동시키는 코루틴
    private IEnumerator EndCutSceneCoroutine()
    {
        yield return mNextCoroutine = FadeManager.instance.StartAndGetCoroutineFadeOut();

        SetEnable(false);
        mImage.raycastTarget = false;

        mNextCoroutine = null;

        SceneManager.LoadScene(SceneInfoManager.dicSceneInfo[mNextScene].SceneName);
    }

    private void SetEnable(bool bEnable)
    {
        mCanvas.enabled = bEnable;
        mDissolveImage.SetDefault();
    }
}
