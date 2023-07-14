using UnityEngine;

public class GameController : MonoSingleTon<GameController>
{
    private Controller[] ControllersNeedToBeInitAtGameStart = new Controller[] {
        new GameStaticsController(new GameStaticsModel(), new View[]{ new GameStaticsView()})
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

    private void OnApplicationQuit()
    {
        foreach (Controller controller in ControllersNeedToBeInitAtGameStart)
        {
            controller.OnDestroy();
        }
    }
}