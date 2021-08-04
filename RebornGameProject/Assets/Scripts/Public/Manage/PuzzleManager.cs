using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 맵 안의 퍼즐이 다 해결됬는지 확인 후 씬 이동
/// </summary>
public class PuzzleManager : MonoBehaviour
{
    [SerializeField] Puzzle[] allPuzzle;    // 맵 안의 모든 퍼즐 목록
    [SerializeField] string nextScene;      // 다음 씬 이름

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어가 아니면 무시
        if (other.CompareTag("Player") == false)
        {
            return;
        }

        // 모든 퍼즐 확인
        foreach(Puzzle puzzle in allPuzzle)
        {
            if (!puzzle.IsEndPuzzle) break;

            // 모든 퍼즐이 완료되었을 경우 씬 이동
            SceneManager.LoadScene(nextScene);
            return;
        }

        // 미완성 퍼즐이 있을때
        Chat.Instance.ActivateChat("아직 마무리하지 않은 일이 있는 것 같다.", null, true);
    }
}