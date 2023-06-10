using UnityEngine;

public class Block : MonoBehaviour
{

  private void OnCollisionEnter2D(Collision2D other)
  {
    OnHit();
  }

  private void DestroySelf()
  {
    gameObject.SetActive(false);
  }

  private void OnHit()
  {
    DestroySelf();
  }

}
