public class SceneInfo
{
    public static string[] SceneName = { "MainMenu", "1-1_Subway", "1-2_Campus", "1-3_Classroom","2.1_Boy'sRoom","2.2Girl'sRoom", "End" };

    public enum EScene
    {
        MainMenu,
        Subway,
        Campus,
        Classroom,
        boysRoom,
        girlsRoom,
        End,
    };


    public static string GetSceneName(EScene scene)
    {
        return SceneName[(int)scene];
    }
}
