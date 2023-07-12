using System;
using System.Collections.Generic;

public class Controller
{
  protected Model _model;
  protected View[] _views;

  public Controller(Model model, View[] views)
  {
    this._model = model;
    this._views = views;
    this._model.BindController(this);
    foreach(View view in _views)
    {
        view.BindController(this);
    }
    
  }

  public virtual void OnDataChange(Dictionary<Enum, object> changedData)
  {
        foreach (View view in _views)
        {
            view.OnDataChange(changedData);
        }
  }

      public virtual void Init()
      {
        // 先初始化view，这样model初始化完数据可以马上更新一次view
        foreach (View view in _views)
        {
            view.Init();
        }
        this._model.Init();
      }

      public virtual void Reset()
      {
        foreach (View view in _views)
        {
            view.Reset();
        }
        _model.Reset();
      }  

    public virtual void OnDestroy()
    {
        foreach (View view in _views)
        {
            view.OnDestroy();
        }
        this._model.OnDestroy();
    }
}
