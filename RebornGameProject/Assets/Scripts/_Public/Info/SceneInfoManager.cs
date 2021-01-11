#pragma warning disable CS0649

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 씬 정보 관리
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

    public enum EScene
    {
        MainMenu,
        Subway,
        Campus,
        Classroom,
        End,
        boysRoom,
        girlsRoom,
        NULL,
    };

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
