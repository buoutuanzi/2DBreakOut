using System;
using System.Collections.Generic;
public abstract class View
{
  protected Controller _controller;
  public abstract void Init();
  public void BindController(Controller controller)
  {
    _controller = controller;
  }

  public abstract void OnDataChange(Dictionary<Enum, object> changedData);
}
