using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BuffMgr : MonoBehaviour
{
    private Dictionary<BuffType, IBuff> _buffType2BuffMap = new Dictionary<BuffType, IBuff>();
    private List<IBuff> _activeBuff = new List<IBuff>();
    private void Awake()
    {
        RegisterAllBuffProcesser();
    }

    // ע��������BuffProcesserMarker����
    public void RegisterAllBuffProcesser()
    {
        GameUtils.GetAllTypeWithTargetAttribute<BuffProcesserMarker>((t, marker) =>
        {
            if(t.GetInterfaces().Contains(typeof(IBuff)))
            {
                _buffType2BuffMap.Add(marker.BuffType, (IBuff)Activator.CreateInstance(t));
            }
            else
            {
                Debug.LogWarning("�����û�м̳�IBuff�����Ǳ��Ϊ��BuffProcesser" + t.Name);
            }
        });
    }

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
            Debug.LogError("Buff ������������");
        }
        BuffTriggerArgs triggerArgs = args as BuffTriggerArgs;
        Debug.Log("Buff ������Type: " + triggerArgs.buffType);
        IBuff triggerBuff = _buffType2BuffMap[triggerArgs.buffType];
        if (!_activeBuff.Contains(triggerBuff))
        {
            _activeBuff.Add(triggerBuff);
        }
        
        triggerBuff.Trigger(triggerArgs);
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
        if (EventBus.hasInstance())
        {
            EventBus.Instance.UnRegisteTo(EventType.OnBuffTrigger, OnTriggerBuff);
            EventBus.Instance.UnRegisteTo(EventType.OnLevelComplete, OnLevelComplete);
        }
        
        foreach (IBuff buff in _activeBuff)
        {
            buff.Destroy();
        }
        _activeBuff.Clear();
    }
}
