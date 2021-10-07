#pragma warning disable CS0649

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 씬 정보 제공
/// </summary>

public class SceneInfoManager : MonoBehaviour
{
    [SerializeField]
    private SceneInfo[] scenes;
    public static Dictionary<EScene, SceneInfo> dicSceneInfo = new Dictionary<EScene, SceneInfo>();

    private void Awake()
    {
        for (int i = 0; i < scenes.Length; ++i)
        {
            dicSceneInfo.Add((EScene)i, scenes[i]);
        }
    }

    // 씬 목록 열거형으로 관리
    public enum EScene
    {
        MainMenu,
        Subway,
        Campus,
        Classroom,
        Manroom,
        End,
        NULL,
    };

    // 인자로 넘어온 문자열과 일치하는 이름의 씬 반환
    public static EScene GetSceneEnumOrNull(string sceneName)
    {
        foreach (KeyValuePair<EScene, SceneInfo> pair in dicSceneInfo)
        {
            if (pair.Value.SceneName == sceneName)
            {
                return pair.Key;
            }
        }

        return EScene.NULL;
    }
}