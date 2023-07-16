using UnityEngine;

[BuffProcesserMarker(BuffType.ChangeBulletVelocity)]
public class ChangeBulletVelocityBuff : IBuff
{
    private float originVelocity;
    private float curSpeedScale = 1;
    private float minScale = 0.5f;
    private float maxScale = 2;
    private BulletShareProperty bulletShareProperty;
    public void Destroy()
    {
        Reset();
    }

    public void Reset()
    {
        UpdateVelocity(originVelocity);
    }

    public void Trigger(BuffTriggerArgs args)
    {
        if (!bulletShareProperty)
        {
            bulletShareProperty = BulletSpawn.Instance.GetBulletShareProperty();
            originVelocity = bulletShareProperty.velocity;
        }
        float scale = (float)args.args;
        curSpeedScale *= scale;
        curSpeedScale = Mathf.Clamp(curSpeedScale, minScale, maxScale);
        UpdateVelocity(originVelocity * curSpeedScale);
    }

    private void UpdateVelocity(float newVelocity)
    {
        if (bulletShareProperty)
        {
            bulletShareProperty.velocity = newVelocity;
            // 修改已激活子弹的速度
            if (BulletSpawn.hasInstance())
            {
                foreach (var bullet in BulletSpawn.Instance.activeBulletSet)
                {
                    bullet.GetComponent<Bullet>().RefreshVelocity();
                }
            }
        }
    }
}
