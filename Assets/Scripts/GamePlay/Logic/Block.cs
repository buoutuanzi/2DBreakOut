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
    DropItem();
    EventBus.Instance.TriggerEvent(EventType.OnBlockDestory, null);
    gameObject.SetActive(false);
  }

  private void DropItem()
  {
    RandomBuffItemSpawn.Instance.SpawnByPosition(transform.position);
  }
}
