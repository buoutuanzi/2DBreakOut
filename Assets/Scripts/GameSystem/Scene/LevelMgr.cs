public class LevelMgr : SingleTon<LevelMgr>
{

  private int _curLevelIndex = -1;
  private void Awake()
  {
    EventBus.Instance.RegisteTo(EventType.OnLevelComplete, ToNextLevel);
  }

  public bool SwitchLevel(int levelIndex)
  {
    string targetSceneName = SceneConfig.GetLevelSceneNameByIndex(levelIndex);
    if (targetSceneName == null)
    {
      return false;
    }

    SceneMgr.Instance.SwitchScene(targetSceneName);
    _curLevelIndex = levelIndex;

    return true;
  }

  public void ToNextLevel(object args)
  {
    if (!SwitchLevel(_curLevelIndex + 1))
    {
      SceneMgr.Instance.SwitchScene(SceneConfig.ENDSCENENAME);
    }
  }

  private void OnDestroy()
  {
    EventBus.Instance.UnRegisteTo(EventType.OnLevelComplete, ToNextLevel);
  }
}
