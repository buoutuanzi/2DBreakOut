using UnityEngine;

public class Block : Defender
{
    public override void OnHit(float attack)
    {
        health -= attack;
        if(health <= 0)
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        EventBus.Instance.TriggerEvent(EventType.OnBlockDestory, null);
        EventBus.Instance.TriggerEvent(EventType.OnItemSpawn, transform);
        gameObject.SetActive(false);
    }
}
