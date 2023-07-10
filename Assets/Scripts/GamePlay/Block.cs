using UnityEngine;

public class Block : MonoBehaviour
{
    private float DropItemChance = 0.4f;
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
        float chance = Random.Range(0f,1f);
        Debug.Log("本次生成概率为：" + chance);
        if(chance <= DropItemChance)
        {
            RandomBuffItemSpawn.Instance.SpawnByPosition(transform.position);
        }
    }

  private void OnHit()
  {
    DestroySelf();
  }

}
