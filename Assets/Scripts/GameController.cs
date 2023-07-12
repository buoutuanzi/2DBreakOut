using UnityEngine;

public class GameController : MonoBehaviour
{
  private Controller[] ControllersNeedToBeInitAtGameStart = new Controller[] {
        new GameStaticsController(new GameStaticsModel(), new GameStaticsView())
    };

  private void Start()
  {
    InitControllers();
  }

  private void InitControllers()
  {
    foreach (Controller controller in ControllersNeedToBeInitAtGameStart)
    {
      controller.Init();
    }
  }

    private void OnDestroy()
    {
        foreach (Controller controller in ControllersNeedToBeInitAtGameStart)
        {
            controller.OnDestroy();
        }
    }
}