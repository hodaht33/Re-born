using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    //private enum SceneName
    //{
    //    Subway = 1,
    //    Campus,
    //    Classroom,
    //}

    [SerializeField] private string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        switch(nextSceneName)
        {
            case "Campus":
                {
                    if (Inventory.Instance.FindItem("Phone") == false)
                    {
                        Chat.Instance.ActivateChat("무언가 더 있을 것이다.", null, true);

                        return;
                    }

                    if (other.CompareTag("Player") == true)
                    {
                        SceneManager.LoadScene(nextSceneName);
                    }

                    break;
                }
            case "Classroom":
                {
                    if (other.CompareTag("Player") == true)
                    {
                        StartCoroutine(PlayCutScene());
                        //SceneManager.LoadScene(nextSceneName);
                    }

                    break;
                }
        }
    }

    private IEnumerator PlayCutScene()
    {
        Coroutine coroutine = FadeManager.Instance.StartAndGetCoroutineFadeOutOrNull();
        if (coroutine == null)
        {
            yield break;
        }

        yield return coroutine;

        CutSceneManager.Instance.PlayCutScene();

        yield return FadeManager.Instance.StartAndGetCoroutineFadeInOrNull();
        //SceneManager.LoadScene(nextSceneName);

        enabled = false;
    }
}
