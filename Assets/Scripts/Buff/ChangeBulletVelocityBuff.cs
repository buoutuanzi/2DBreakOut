using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBulletVelocityBuff : IBuff
{
    private float originShootForce = 0.0f;
    private float curSpeedScale = 1;
    private float minScale = 0.5f;
    private float maxScale = 2;
    private Shooter shooter = null;
    const string shooterPath = "GameView/Shooter";
    public void Destroy()
    {
        Reset();
        shooter = null;
    }

    public void Reset()
    {
        UpdateVelocityByScale(1 / curSpeedScale);
        shooter = null;
    }

    public void Trigger(BuffTriggerArgs args)
    {
        if (!shooter)
        {
            shooter = GameObject.Find(shooterPath).GetComponent<Shooter>();
            originShootForce = shooter.defaultShootForce;
        }

        UpdateVelocityByScale((float)args.args);
    }

    private void UpdateVelocityByScale(float scale)
    {
        float oldScale = curSpeedScale;
        curSpeedScale *= scale;
        curSpeedScale = Mathf.Clamp(curSpeedScale, minScale, maxScale);

        // �޸ķ������ķ�������
        if (shooter)
        {
            shooter.defaultShootForce = curSpeedScale * originShootForce;
        }
        // �޸��Ѽ����ӵ����ٶ�
        float deltaScale = curSpeedScale / oldScale;
        if (BulletSpawn.hasInstance())
        {
            foreach (var bullet in BulletSpawn.Instance.activeBulletSet)
            {
                bullet.GetComponent<Bullet>().SetVelocityByScaleRefNow(deltaScale);
            }
        }
        
    }
}
