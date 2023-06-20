using UnityEngine;
using UnityEngine.UI;

public class GameStartUI : MonoBehaviour
{
  private Button _btnGameStart;
  private string _btnGameStartName = "GameStartBtn";
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
  }

  private void BindEvents()
  {
    _btnGameStart.onClick.AddListener(OnGameStart);
  }

  private void OnGameStart()
  {
    LevelMgr.Instance.SwitchLevel(_defaultLevelIndex);
  }
}
