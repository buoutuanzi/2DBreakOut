public class SceneConfig
{

  private static string[] _LevelSceneList = new string[]{
    "LEVELSCENE_1",
    };

  public static string GetLevelSceneNameByIndex(int levelIndex)
  {
    if (levelIndex >= _LevelSceneList.Length || levelIndex < 0)
    {
      return "ENDSCENE";
    }

    return _LevelSceneList[levelIndex];
  }
}
