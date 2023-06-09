using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffCollectable : Collidable, IReuseableItem
{
    public BuffType buffType;
    public object args = null;
    public override void OnCollidableEnter(CollisionPannel other)
    {
        BuffTriggerArgs buffTriggerArgs = new BuffTriggerArgs(buffType, args);
        EventBus.Instance.TriggerEvent(EventType.OnBuffTrigger, buffTriggerArgs);
        Return();
    }

    public void Return()
    {
        if (RandomBuffItemSpawn.hasInstance())
        {
            RandomBuffItemSpawn.Instance.Return(gameObject);
        }  
    }
}
