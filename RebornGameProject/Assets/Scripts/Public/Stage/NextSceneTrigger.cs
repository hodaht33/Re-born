#pragma warning disable CS0649

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 다음 씬 이동 트리거
/// </summary>
public class NextSceneTrigger : MonoBehaviour
{
    public delegate bool CheckRequiredItems();
    public event CheckRequiredItems OnCheckRequiredItems;   // LevelPuzzle에서 필요 아이템을 검사하는 CheckRequiredItem메서드 등록
    public delegate bool CheckSuccessAllPuzzles();
    public event CheckSuccessAllPuzzles OnCheckSuccessAllPuzzles;   // LevelPuzzle에서 퍼즐 달성 여부를 검사하는 CheckSuccessAllPuzzles메서드 등록
    public delegate void EndLevel();
    public event EndLevel OnEndLevel;

    [SerializeField]
    private SceneInfoManager.EScene mNextScene; // 이동할 다음 씬
    [SerializeField]
    private bool mbActiveCutScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        // 필요 아이템이 없는 경우
        if (OnCheckRequiredItems != null
            && OnCheckRequiredItems() == false)
        {
            Chat.Instance.ActivateChat("무언가 더 있을 것이다.", null, true);

            return;
        }

        // 퍼즐을 모두 수행하지 않은 경우
        if (OnCheckSuccessAllPuzzles != null
            && OnCheckSuccessAllPuzzles() == false)
        {
            // 확인을 위해 임시로 적어둔 부분
            Chat.Instance.ActivateChat("아직 마무리하지 않은 일이 있는 것 같다.", null, true);

            return;
        }

        // OnEndLevel이 null이 아니면 등록된 이벤트
        OnEndLevel?.Invoke();

        if (mbActiveCutScene == true)
        {
            CutSceneManager.Instance.PlayCutScene(mNextScene);
        }
        else
        {
            SceneManager.LoadScene(SceneInfoManager.dicSceneInfo[mNextScene].SceneName);
        }
    }
}
