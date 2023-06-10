using UnityEngine;

public class DeadZone : MonoBehaviour
{
  private void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.GetComponent<Bullet>())
    {
      BulletSpwan.Instance.Return(other.gameObject);
    }
  }
}
