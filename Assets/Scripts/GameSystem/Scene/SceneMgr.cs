using UnityEngine.SceneManagement;
public class SceneMgr : SingleTon<SceneMgr>
{
  private string _curSceneName = null;
  public void SwitchScene(string sceneName)
  {
    if (sceneName == null || sceneName.Equals(_curSceneName))
    {
      return;
    }

    SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
  }
}
