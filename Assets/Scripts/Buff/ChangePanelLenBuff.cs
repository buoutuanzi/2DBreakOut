using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BuffProcesserMarker(BuffType.ChangePanelLen)]
// 每个buff类实现一类buff功能，每个buff类可以重复触发
public class ChangePannelLenBuff : IBuff
{
    private float curScale = 1;
    const string pannelPath = "GameView/Shooter";
    private GameObject pannel = null;
    private float maxScale = 2;
    private float minScale = 0.5f;
    private CollisionPannel logicPannel = null;
    private CollisionPannelVisual visualPannel = null;
    public void Destroy()
    {
        Reset();
    }

    public void Reset()
    {
        if(pannel != null)
        {
            SetPannelLen(1);
            pannel = null;
        }
    }

    public void Trigger(BuffTriggerArgs args)
    {
        if(pannel == null)
        {
            pannel = GameObject.Find(pannelPath);
            logicPannel = pannel.GetComponent<CollisionPannel>();
            visualPannel = pannel.GetComponent<CollisionPannelVisual>();
        }

        if(pannel != null)
        {
            SetPannelLen((float)args.args);
        }
    }

    private void SetPannelLen(float scale)
    {
        curScale *= scale;
        curScale = Mathf.Clamp(curScale, minScale, maxScale);
        logicPannel?.SetLenScaleRefOriginLen(curScale);
        visualPannel?.SetLenScaleRefOriginLen(curScale);
    }
}
