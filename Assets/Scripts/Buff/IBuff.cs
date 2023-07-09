using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuff
{
    public void Reset();
    public void Trigger(BuffTriggerArgs args);
    public void Destroy();
}
