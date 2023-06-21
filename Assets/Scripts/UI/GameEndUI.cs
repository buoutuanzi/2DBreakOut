using UnityEngine;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
  private Button _btnReturnMainMenu;
  private const string BTNRETURNMAINMENUPATH = "btnReturnToMainMenu";

  [System.Obsolete]
  private void Awake()
  {
    FindComponent();
    BindEvents();

  }

  [System.Obsolete]
  private void FindComponent()
  {
    _btnReturnMainMenu = transform.FindChild(BTNRETURNMAINMENUPATH).GetComponent<Button>();
  }

  private void BindEvents()
  {
    _btnReturnMainMenu.onClick.AddListener(RetunrToMainMenu);
  }

  private void RetunrToMainMenu()
  {
    SceneMgr.Instance.SwitchScene(SceneConfig.STARTSCENENAME);
  }

  private void OnDestroy()
  {
    UnBindEvents();
    _btnReturnMainMenu = null;
  }

  private void UnBindEvents()
  {
    _btnReturnMainMenu.onClick.RemoveListener(RetunrToMainMenu);
  }

}
