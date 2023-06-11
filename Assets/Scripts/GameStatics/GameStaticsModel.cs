using System;
using UnityEngine;
using System.Collections.Generic;
public class GameStaticsModel : Model
{
  private int _leftBlock;
  const string BLOCK_ROOT_NAME = "Blocks";

  public override void Init()
  {
    InitData();
    BindEvents();
  }

  private void InitData()
  {
    GameObject blockRoot = GameObject.Find(BLOCK_ROOT_NAME);
    _leftBlock = blockRoot.transform.childCount;
    UpdateData();
  }

  private void BindEvents()
  {
    EventBus.Instance.RegisteTo(EventType.OnBlockDestory, MinusBlock);
  }

  public void MinusBlock(object args)
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
