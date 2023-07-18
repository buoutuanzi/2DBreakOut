using UnityEngine;

public class Block : Defender
{
    private float curHealth;
    private BlockVisual blockVisual;

    private void Awake()
    {
        curHealth = health;
    }
    private void Start()
    {
        blockVisual = GetComponent<BlockVisual>();
    }

    public override void OnHit(float attack)
    {
        curHealth -= attack;
        blockVisual.UpdateCrackVisual((1 - curHealth / health));
        if(curHealth <= 0)
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
