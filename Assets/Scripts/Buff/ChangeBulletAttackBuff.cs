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
        Debug.Log("�����������ӵ�������");
    }

    public void Trigger(BuffTriggerArgs args)
    {
        Debug.Log("������ı��ӵ�������");
    }

    private void ChangeBulletAttackByScale(float scale)
    {

    }
}
