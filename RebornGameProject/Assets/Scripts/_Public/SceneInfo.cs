public static class SceneInfo
{
    public static string[] SceneName = { "MainMenu", "1-1_Subway", "1-2_Campus", "1-3_Classroom", "End" };
    
    public enum EScene
    {
        MainMenu,
        Subway,
        Campus,
        Classroom,
        End,
    };

    
    public static string GetSceneName(EScene scene)
    {
        return SceneName[(int)scene];
    }
}
