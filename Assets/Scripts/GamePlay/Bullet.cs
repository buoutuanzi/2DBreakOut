using UnityEngine;

public class Bullet : MonoBehaviour
{
  Rigidbody2D _rig;
  private void Awake()
  {
    _rig = GetComponent<Rigidbody2D>();
  }

  public void Shoot(Vector2 dir, float force)
  {
    _rig.AddForce(dir * force);
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    OnHit();
  }

  private void OnHit()
  {
    return;
  }
}
