using System;
using System.Collections.Generic;

public class Controller
{
  protected Model _model;
  protected View _view;

  public Controller(Model model, View view)
  {
    this._model = model;
    this._view = view;
    this._model.BindController(this);
    this._view.BindController(this);
  }

  public virtual void OnDataChange(Dictionary<Enum, object> changedData)
  {
    _view.OnDataChange(changedData);
  }

      public virtual void Init()
      {
        // 先初始化view，这样model初始化完数据可以马上更新一次view
        this._view.Init();
        this._model.Init();
      }

      public virtual void Reset()
      { 
          _view.Reset();
          _model.Reset();
      }  

    public virtual void OnDestroy()
    {
        this._view.OnDestroy();
        this._model.OnDestroy();
    }
}
