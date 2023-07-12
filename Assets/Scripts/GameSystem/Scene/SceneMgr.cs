using System.Collections;
using System.ComponentModel;
using UnityEngine.SceneManagement;
public class SceneMgr : SingleTon<SceneMgr>
{
  private string _curSceneName = null;
  public delegate void OnSceneLoaded();
  OnSceneLoaded _callBack = null;
  public void SwitchScene(string sceneName, OnSceneLoaded callback)
  {
    if (sceneName == null || sceneName.Equals(_curSceneName))
    {
      return;
    }
    _callBack = callback;

    StartCoroutine(SwitchSceneAsync(sceneName));
  }

  IEnumerator SwitchSceneAsync(string sceneName)
    {
        UnityEngine.AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        _callBack?.Invoke();
        _callBack = null;
    }
}
