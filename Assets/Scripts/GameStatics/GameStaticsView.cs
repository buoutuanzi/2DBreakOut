using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStaticsView : View
{
  private TMP_Text _leftBlockCounterText;
  private const string COUNTER_TEXT_PATH = "/GameUI/GameStaticsUI/LeftBlockCounterText";
  private const string COUNTER_TEXT_PREFFIX = "LeftBlock:";

  public override void Init()
  {
    BindUI(null);
  }

  private void BindUI(object args)
  {
    _leftBlockCounterText = GameObject.Find(COUNTER_TEXT_PATH).GetComponent<TMP_Text>();
    UpdateLeftBlockUI(0);
  }

  public override void OnDataChange(Dictionary<Enum, object> changedData)
  {
    int leftBlock = (int)changedData[GameStaticsEnum.LeftBlockValue];
    UpdateLeftBlockUI(leftBlock);
  }

  private void UpdateLeftBlockUI(int leftBlock)
  {
    _leftBlockCounterText.text = COUNTER_TEXT_PREFFIX + leftBlock;
  }

    public override void OnDestroy()
    {
        _leftBlockCounterText = null;
    }

    public override void Reset()
    {
        BindUI(null);
    }
}
