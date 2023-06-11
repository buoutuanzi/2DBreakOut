using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStaticsView : View
{
  private TMP_Text _leftBlockCounterText;
  private const string COUNTER_TEXT_PATH = "/UI/GameStaticsUI/LeftBlockCounterText";
  private const string COUNTER_TEXT_PREFFIX = "LeftBlock:";

  public override void Init()
  {
    BindUI();
  }

  private void BindUI()
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
}
