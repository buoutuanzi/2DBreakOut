using UnityEngine;
using System;

public class Block : MonoBehaviour
{
  Action OnBlockHit;

  private void OnCollisionEnter2D(Collision2D other)
  {
    OnHit();
  }

  private void DestroySelf()
  {
    OnBlockHit = null;
    gameObject.SetActive(false);
  }

  private void OnHit()
  {
    OnBlockHit?.Invoke();
    DestroySelf();
  }

}
