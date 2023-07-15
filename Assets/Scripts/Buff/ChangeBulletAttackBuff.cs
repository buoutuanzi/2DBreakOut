using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BuffProcesserMarker(BuffType.ChangeBulletAttack)]
public class ChangeBulletAttackBuff : IBuff
{
    private float originAttack;
    private float curScale;
    public void Destroy()
    {
        Reset();
    }

    public void Reset()
    {
        Debug.Log("在这里重置子弹攻击力");
    }

    public void Trigger(BuffTriggerArgs args)
    {
        Debug.Log("在这里改变子弹攻击力");
    }

    private void ChangeBulletAttackByScale(float scale)
    {

    }
}
