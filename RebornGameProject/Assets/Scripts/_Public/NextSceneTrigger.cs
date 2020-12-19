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
    [SerializeField]
    private SceneInfo.EScene mNextScene;

    private void OnTriggerEnter(Collider other)
    {
        switch(mNextScene)
        {
            case SceneInfo.EScene.Campus:
                {
                    if (Inventory.Instance.FindItem("Phone") == false)
                    {
                        Chat.Instance.ActivateChat("무언가 더 있을 것이다.", null, true);

                        return;
                    }

                    if (other.CompareTag("Player") == true)
                    {
                        SceneManager.LoadScene(SceneInfo.GetSceneName(mNextScene));
                    }

                    break;
                }
            case SceneInfo.EScene.Classroom:
                {
                    if (other.CompareTag("Player") == true)
                    {
                        StartCoroutine(PlayCutSceneCoroutine());
                    }

                    break;
                }
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

        CutSceneManager.Instance.PlayCutScene();    // 컷씬 화면으로 전환

        // 전환 후 페이드 인
        yield return FadeManager.Instance.StartAndGetCoroutineFadeInOrNull();

        // 페이드 인 끝나면 비활성화(안먹히는 부분으로 생각됨)
        enabled = false;
    }
}
