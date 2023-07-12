public abstract class Model
{

  protected Controller _controller;
  public abstract void Init();
  public abstract void Reset();
  public void BindController(Controller controller)
  {
    _controller = controller;
  }

  public abstract void OnDestroy();

}
