using UnityEngine;

public class CollisionPanel : MonoBehaviour
{
  private float DirScale = 4.0f;
  private void OnCollisionExit2D(Collision2D other)
  {
    Bullet bullet = other.gameObject.GetComponent<Bullet>();
    if (bullet != null)
    {
      ReflectBulletByRefPos(other);
    }
  }

  private void ReflectBulletByRefPos(Collision2D other)
  {
    float diffX = other.transform.position.x - transform.position.x;
    Vector2 BiasVec = new Vector2(diffX * DirScale, 0);
    Vector2 oldVelocityDir = other.rigidbody.velocity;
    float velocityMag = oldVelocityDir.magnitude;
    Vector2 newVelocityDir = (oldVelocityDir + BiasVec).normalized;
    other.rigidbody.velocity = newVelocityDir * velocityMag;
  }
}
