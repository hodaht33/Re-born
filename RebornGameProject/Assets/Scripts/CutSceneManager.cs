using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutSceneManager : SingletonBase<CutSceneManager>
{
    private Canvas canvas;
    private Image image;
    private int currentCutSceneIndex;
    private int spriteIndex;
    [SerializeField] private DissolveImage dissolveImage;

    // 한 챕터 컷씬 이미지들을 가지는 구조체
    [System.Serializable]
    struct CutScene
    {
        [SerializeField] public Sprite[] sprites;
    }

    // 모든 컷씬 에디터로 관리
    [SerializeField] CutScene[] cutScenes;

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

        canvas = GetComponent<Canvas>();
        image = transform.Find("SceneImage").GetComponent<Image>();
    }

    public void PlayCutScene()
    {
        canvas.enabled = true;
        image.raycastTarget = true;
        spriteIndex = 0;
        image.sprite = cutScenes[currentCutSceneIndex].sprites[spriteIndex];
    }

    private Coroutine nextCoroutine = null;
    public void NextCutScene()
    {
        ++spriteIndex;

        if (nextCoroutine == null)
        {
            if (spriteIndex >= cutScenes[currentCutSceneIndex].sprites.Length)
            {
                nextCoroutine = StartCoroutine(EndCutScene());

                return;
            }

            image.sprite = cutScenes[currentCutSceneIndex].sprites[spriteIndex];
        }
    }

    private IEnumerator EndCutScene()
    {
        dissolveImage.StartDissolve();

        yield return FadeManager.instance.StartCoroutineFadeOut();

        SetEnable(false);
        image.raycastTarget = false;

        nextCoroutine = null;

        switch (currentCutSceneIndex)
        {
            case 0:
                ++currentCutSceneIndex;
                SceneManager.LoadScene("Subway");
                break;
            case 1:
                ++currentCutSceneIndex;
                SceneManager.LoadScene("Classroom");
                break;
        }
    }

    private void SetEnable(bool isEnable)
    {
        canvas.enabled = isEnable;
        dissolveImage.SetDefault();
    }
}
