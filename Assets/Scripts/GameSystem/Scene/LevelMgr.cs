public class LevelMgr : SingleTon<LevelMgr>
{
  public int CurLevel { get { return _curLevelIndex + 1; } }
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

    SceneMgr.Instance.SwitchScene(targetSceneName, OnSceneLoaded);
    _curLevelIndex = levelIndex;
    return true;
  }

    private void OnSceneLoaded()
    {
        EventBus.Instance.TriggerEvent(EventType.OnLevelBegin, null);
    }

  public void ToNextLevel(object args)
  {
    if (!SwitchLevel(_curLevelIndex + 1))
    {
      SceneMgr.Instance.SwitchScene(SceneConfig.ENDSCENENAME, null);
    }
  }

  private void OnDestroy()
  {
    EventBus.Instance.UnRegisteTo(EventType.OnLevelComplete, ToNextLevel);
  }
}
