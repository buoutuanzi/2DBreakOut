
public class GameStaticsController : Controller
{
  public GameStaticsController(Model model, View[] views) : base(model, views)
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
        if (EventBus.hasInstance())
        {
            EventBus.Instance.RegisteTo(EventType.OnLevelBegin, ResetThis);
        }
        
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
