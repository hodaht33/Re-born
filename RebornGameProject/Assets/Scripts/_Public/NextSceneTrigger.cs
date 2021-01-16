#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 다음 씬 이동 트리거
/// </summary>
public class NextSceneTrigger : MonoBehaviour
{
    // TODO : Level의 함수를 이벤트함수로 2개 받아 플레이어 충돌 시에 검사 
    public delegate bool CheckRequiredItems();
    public event CheckRequiredItems OnCheckRequiredItems;
    public delegate bool CheckSuccessAllPuzzles();
    public event CheckSuccessAllPuzzles OnCheckSuccessAllPuzzles;
    public delegate void EndLevel();
    public event EndLevel OnEndLevel;

    [SerializeField]
    private SceneInfoManager.EScene mNextScene;
    [SerializeField]
    private bool mbActiveCutScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        if (OnCheckRequiredItems != null
            && OnCheckRequiredItems() == false)
        {
            Chat.Instance.ActivateChat("무언가 더 있을 것이다.", null, true);

            return;
        }

        if (OnCheckSuccessAllPuzzles != null
            && OnCheckSuccessAllPuzzles() == false)
        {
            return;
        }

        OnEndLevel?.Invoke();

        if (mbActiveCutScene == true)
        {
            CutSceneManager.Instance.PlayCutScene(mNextScene);
            //StartCoroutine(PlayCutSceneCoroutine());
        }
        else
        {
            SceneManager.LoadScene(SceneInfoManager.dicSceneInfo[mNextScene].SceneName);
        }
    }

    private IEnumerator PlayCutSceneCoroutine()
    {
        // 페이드 아웃
        Coroutine coroutine = FadeManager.Instance.StartAndGetCoroutineFadeOutOrNull();

        if (coroutine == null)
        {
            yield break;
        }

        yield return coroutine;

        CutSceneManager.Instance.PlayCutScene(mNextScene);    // 컷씬 화면으로 전환

        // 전환 후 페이드 인
        yield return FadeManager.Instance.StartAndGetCoroutineFadeInOrNull();

        // 페이드 인 끝나면 비활성화(안먹히는 부분으로 생각됨)
        enabled = false;
    }
}
