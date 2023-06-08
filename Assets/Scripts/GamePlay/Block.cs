using UnityEngine;
using System;

public class Block : MonoBehaviour
{
  Action OnBlockHit;

  private void OnCollisionEnter2D(Collision2D other)
  {
    OnBlockHit?.Invoke();
    DestroySelf();
  }

  void DestroySelf()
  {
    OnBlockHit = null;
    gameObject.SetActive(false);
  }
}
