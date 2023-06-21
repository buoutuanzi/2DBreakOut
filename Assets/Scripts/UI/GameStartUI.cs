using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
  private Button _btnGameStart = null;
  private string _btnGameStartName = "GameStartBtn";
  private Button _btnQuitGame = null;
  private string _btnQuitGameName = "QuitGameBtn";
  private int _defaultLevelIndex = 0;

  [System.Obsolete]
  private void Awake()
  {
    FindComponent();
    BindEvents();
  }

  [System.Obsolete]
  private void FindComponent()
  {
    _btnGameStart = transform.FindChild(_btnGameStartName).GetComponent<Button>();
    _btnQuitGame = transform.FindChild(_btnQuitGameName).GetComponent<Button>();
  }

  private void BindEvents()
  {
    _btnGameStart.onClick.AddListener(OnGameStart);
    _btnQuitGame.onClick.AddListener(QuitGame);
  }

  private void OnGameStart()
  {
    LevelMgr.Instance.SwitchLevel(_defaultLevelIndex);
  }
  private void QuitGame()
  {
    Application.Quit();
  }

  private void OnDestroy()
  {
    UnBindEvents();
    _btnGameStart = null;
    _btnQuitGame = null;
  }

  private void UnBindEvents()
  {
    _btnGameStart.onClick.RemoveListener(OnGameStart);
    _btnQuitGame.onClick.RemoveListener(QuitGame);
  }
}
