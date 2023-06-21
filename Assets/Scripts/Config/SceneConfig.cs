public class SceneConfig
{
  public const string ENDSCENENAME = "EndScene";
  public const string STARTSCENENAME = "StartScene";
  private static string[] _LevelSceneList = new string[]{
    "LEVELSCENE_1",
    };

  public static string GetLevelSceneNameByIndex(int levelIndex)
  {
    if (levelIndex >= _LevelSceneList.Length || levelIndex < 0)
    {
      return null;
    }

    return _LevelSceneList[levelIndex];
  }
}
