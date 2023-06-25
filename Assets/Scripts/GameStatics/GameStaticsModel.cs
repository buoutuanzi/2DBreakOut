using System;
using UnityEngine;
using System.Collections.Generic;
public class GameStaticsModel : Model
{
  private int _leftBlock;
  const string BLOCK_ROOT_NAME = "Blocks";

  public override void Init()
  {
    InitData(null);
    BindEvents();
  }

  private void InitData(object args)
  {
    GameObject blockRoot = GameObject.Find(BLOCK_ROOT_NAME);
    _leftBlock = blockRoot.transform.childCount;
    UpdateData();
  }

  private void BindEvents()
  {
    EventBus.Instance.RegisteTo(EventType.OnBlockDestory, MinusBlock);
    EventBus.Instance.RegisteTo(EventType.OnLevelBegin, InitData);
  }

  private void MinusBlock(object args)
  {
    _leftBlock--;

    UpdateData();

    if (_leftBlock == 0)
    {
      EventBus.Instance.TriggerEvent(EventType.OnLevelComplete, null);
    }
  }

  private void UpdateData()
  {
    _controller.OnDataChange(new Dictionary<Enum, object>(){
        {GameStaticsEnum.LeftBlockValue, _leftBlock}
    });
  }
}
