using System.Collections;
using System.Collections.Generic;
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
        public Sprite[] mSprites;
    }

    // 모든 컷씬 에디터로 관리
    [SerializeField]
    private CutScene[] mCutScenes;
    private Coroutine mNextCoroutine;

    public void PlayCutScene()
    {
        mCanvas.enabled = true;
        mImage.raycastTarget = true;
        mSpriteIndex = 0;
        mImage.sprite = mCutScenes[mCurrentCutSceneIndex].mSprites[mSpriteIndex];
    }

    public void NextCutScene()
    {
        ++mSpriteIndex;

        if (mNextCoroutine == null)
        {
            if (mSpriteIndex >= mCutScenes[mCurrentCutSceneIndex].mSprites.Length)
            {
                mDissolveImage.StartDissolve();

                mNextCoroutine = StartCoroutine(EndCutSceneCoroutine());

                return;
            }

            mImage.sprite = mCutScenes[mCurrentCutSceneIndex].mSprites[mSpriteIndex];
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

        switch (mCurrentCutSceneIndex)
        {
            case 0:
                ++mCurrentCutSceneIndex;
                SceneManager.LoadScene("Subway");
                break;
            case 1:
                ++mCurrentCutSceneIndex;
                SceneManager.LoadScene("Classroom");
                break;
        }
    }

    private void SetEnable(bool bEnable)
    {
        mCanvas.enabled = bEnable;
        mDissolveImage.SetDefault();
    }
}
