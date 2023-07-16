using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttacker : CollideAttacker
{
    private BulletShareProperty shareProperty;
    private void Awake()
    {
        Bullet bullet = GetComponent<Bullet>();
        if (bullet)
        {
            shareProperty = bullet.shareProperty;
        }
    }
    public override float GetAttack()
    {
        return shareProperty.attack;
    }
}
