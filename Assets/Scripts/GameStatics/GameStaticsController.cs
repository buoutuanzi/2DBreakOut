using JetBrains.Annotations;

public class GameStaticsController : Controller
{
  public GameStaticsController(Model model, View view) : base(model, view)
  {
  }

    public override void Init()
    {
       base.Init();
       BindEvents();
    }

    private void BindEvents()
    {
        EventBus.Instance.RegisteTo(EventType.OnLevelBegin, ResetThis);
    }

    private void UnBindEvents()
    {
        EventBus.Instance.RegisteTo(EventType.OnLevelBegin, ResetThis);
    }

    private void ResetThis(object args)
    {
        Reset();
    }

    public override void OnDestroy()
    {
        UnBindEvents();
        base.OnDestroy();
    }
}
