using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BuffMgr : MonoBehaviour
{
    private Dictionary<BuffType, IBuff> _buffType2BuffMap = new Dictionary<BuffType, IBuff>()
    {

    };

    private List<IBuff> _activeBuff = new List<IBuff>();
    // Start is called before the first frame update
    void Start()
    {
        EventBus.Instance.RegisteTo(EventType.OnBuffTrigger, OnTriggerBuff);
        EventBus.Instance.RegisteTo(EventType.OnLevelComplete, OnLevelComplete);
    }

    private void OnTriggerBuff(object args)
    {
        if(!(args is BuffTriggerArgs))
        {
            Debug.LogError("Buff ´¥·¢²ÎÊý´íÎó");
        }
        BuffTriggerArgs triggerArgs = args as BuffTriggerArgs;

        IBuff triggerBuff = _buffType2BuffMap[triggerArgs.buffType];
        _activeBuff.Add(triggerBuff);
        triggerBuff.Trigger();
    }

    private void OnLevelComplete(object args)
    {
        foreach(IBuff buff in _activeBuff)
        {
            buff.Reset();
        }
        _activeBuff.Clear();
    }

    private void OnDestroy()
    {
        EventBus.Instance.UnRegisteTo(EventType.OnBuffTrigger, OnTriggerBuff);
        EventBus.Instance.UnRegisteTo(EventType.OnLevelComplete, OnLevelComplete);
        foreach (IBuff buff in _activeBuff)
        {
            buff.Destroy();
        }
        _activeBuff.Clear();
    }
}
