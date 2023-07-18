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
    EventBus.Instance.TriggerEvent(EventType.OnItemSpawn, transform.position);
    EventBus.Instance.TriggerEvent(EventType.OnBlockDestory, null);
    gameObject.SetActive(false);
  }
}
