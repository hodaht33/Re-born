using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 맵 안의 기믹이 다 해결됬는지 확인 후 씬 이동
/// </summary>

// 씬 종류
public enum SceneName
{
    Subway,
    Campus,
    Classroom,
    Manroom
};

public class MapManager : MonoBehaviour
{
    [SerializeField] Puzzle[] allPuzzle;    // 맵 안의 모든 퍼즐 목록
    [SerializeField] SceneName nextScene;   // 다음 씬 이름  

    private ConcurrentDictionary<SceneName, string> SceneRealName;  // 씬 이동을 위한 실제 이름
    
    private void Start()
    {
        SceneRealName = new ConcurrentDictionary<SceneName, string>();

        // 직접 씬 이름 매칭
        SceneRealName[SceneName.Subway] = "1-1_Subway";
        SceneRealName[SceneName.Campus] = "1-2_Campus";
        SceneRealName[SceneName.Classroom] = "1-3_Classroom";
        SceneRealName[SceneName.Manroom] = "1-4_Manroom";
    }

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
            SceneManager.LoadScene(SceneRealName[nextScene]);
            return;
        }

        // 미완성 퍼즐이 있을때
        Chat.Instance.ActivateChat("아직 마무리하지 않은 일이 있는 것 같다.", null, true);
    }
}