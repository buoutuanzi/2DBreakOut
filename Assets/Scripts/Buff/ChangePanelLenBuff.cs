using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ÿ��buff��ʵ��һ��buff���ܣ�ÿ��buff������ظ�����
public class ChangePanelLenBuff : IBuff
{
    private float curScale = 1;
    const string PannelPath = "GameView/Shooter";
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
        }
    }

    public void Trigger(BuffTriggerArgs args)
    {
        if(pannel == null)
        {
            pannel = GameObject.Find(PannelPath);
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