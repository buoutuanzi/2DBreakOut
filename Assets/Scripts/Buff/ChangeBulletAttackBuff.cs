using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BuffProcesserMarker(BuffType.ChangeBulletAttack)]
public class ChangeBulletAttackBuff : IBuff
{
    private float originAttack;
    private BulletShareProperty bulletShareProperty = null;
    public void Destroy()
    {
        Reset();
    }

    public void Reset()
    {
        if (bulletShareProperty)
        {
            bulletShareProperty.attack = originAttack;
        }
    }

    public void Trigger(BuffTriggerArgs args)
    {
        if (!bulletShareProperty)
        {
            bulletShareProperty = BulletSpawn.Instance.GetBulletShareProperty();
            originAttack = bulletShareProperty.attack;
        }
        bulletShareProperty.attack += (float)args.args;
    }
}
