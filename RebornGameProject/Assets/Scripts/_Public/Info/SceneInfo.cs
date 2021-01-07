#pragma warning disable CS0649

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 작성자 : 이성호
/// 기능 : 씬 정보 관리
/// </summary>
public class SceneInfo : MonoBehaviour
{
    [SerializeField]
    //private SceneAsset[] scenes;
    private string[] scenes;
    private static string[] sceneNames;
    private static Dictionary<EScene, string> dicSceneInfo = new Dictionary<EScene, string>();

    private void Awake()
    {
        sceneNames = new string[scenes.Length];
        for (int i = 0; i < scenes.Length; ++i)
        {
            if (scenes[i] != null)
            {
                //sceneNames[i] = scenes[i].name;
                sceneNames[i] = scenes[i];
                dicSceneInfo.Add((EScene)i, sceneNames[i]);
            }
            else
            {
                sceneNames[i] = "";
                dicSceneInfo.Add((EScene)i, sceneNames[i]);
            }
        }
    }

    public enum EScene
    {
        MainMenu,
        Subway,
        Campus,
        Classroom,
        boysRoom,
        girlsRoom,
        End,
        NULL,
    };

    public static string GetSceneName(EScene scene)
    {
        return dicSceneInfo[scene];
    }

    public static EScene GetSceneEnum(string sceneName)
    {
        for (int i = 0; i < sceneNames.Length; ++i)
        {
            if (sceneNames[i].Equals(sceneName) == true)
            {
                return (EScene)i;
            }
        }

        return EScene.NULL;
    }
}
