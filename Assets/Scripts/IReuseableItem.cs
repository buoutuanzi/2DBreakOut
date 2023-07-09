using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 可以被DeadZone回收的物品
public interface IReuseableItem
{
    public void Return();
}
