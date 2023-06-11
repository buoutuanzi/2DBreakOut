using UnityEngine;

public class Block : MonoBehaviour
{

  private void OnCollisionEnter2D(Collision2D other)
  {
    OnHit();
  }

  private void DestroySelf()
  {
    EventBus.Instance.TriggerEvent(EventType.OnBlockDestory, null);
    gameObject.SetActive(false);
  }

  private void OnHit()
  {
    DestroySelf();
  }

}
