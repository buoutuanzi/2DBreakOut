using System;
using System.Collections.Generic;
public abstract class View
{
  protected Controller _controller;
  // 创建时初始化，只会调用一次
  public abstract void Init();
  // 关卡切换时重新初始化，会多次调用
  public abstract void Reset();
  public abstract void OnDestroy();
  public void BindController(Controller controller)
  {
    _controller = controller;
  }

  public abstract void OnDataChange(Dictionary<Enum, object> changedData);
}
