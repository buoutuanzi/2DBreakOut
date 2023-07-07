using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffTriggerArgs
{
    public BuffType buffType;
    public object args;

    public BuffTriggerArgs(BuffType buffType, object args)
    {
        this.buffType = buffType;
        this.args = args;
    }
}
