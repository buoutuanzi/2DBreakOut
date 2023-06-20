using UnityEngine.SceneManagement;
public class LevelMgr : SingleTon<LevelMgr>
{
  private int _curLevelIndex = -1;
  private void Awake()
  {
    EventBus.Instance.RegisteTo(EventType.OnLevelComplete, ToNextLevel);
  }

  public void SwitchLevel(int levelIndex)
  {
    if (_curLevelIndex == levelIndex)
    {
      return;
    }

    SceneManager.LoadScene(SceneConfig.GetLevelSceneNameByIndex(levelIndex), LoadSceneMode.Single);
    _curLevelIndex = levelIndex;
  }

  public void ToNextLevel(object args)
  {
    SwitchLevel(_curLevelIndex + 1);
  }

  private void OnDestroy()
  {
    EventBus.Instance.UnRegisteTo(EventType.OnLevelComplete, ToNextLevel);
  }
}
