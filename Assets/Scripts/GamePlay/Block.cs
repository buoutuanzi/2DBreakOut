using UnityEngine;

public class Block : MonoBehaviour
{
    private float DropItemChance = 0.8f;
  private void OnCollisionEnter2D(Collision2D other)
  {
    OnHit();
  }

  private void DestroySelf()
  {
   RandomDropItem();
    EventBus.Instance.TriggerEvent(EventType.OnBlockDestory, null);
    gameObject.SetActive(false);
  }

    private void RandomDropItem()
    {
        float chance = Random.Range(0,1);
        if(chance <= DropItemChance)
        {
            RandomItemSpawn.Instance.SpawnByPosition(transform.position);
        }
    }

  private void OnHit()
  {
    DestroySelf();
  }

}
