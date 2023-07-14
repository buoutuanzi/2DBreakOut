using UnityEngine;

public class Block : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D other)
  {
    OnHit();
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

  private void OnHit()
  {
    DestroySelf();
  }

}
