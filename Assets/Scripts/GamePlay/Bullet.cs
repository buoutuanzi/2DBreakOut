using UnityEngine;

public class Bullet : MonoBehaviour
{
  Rigidbody2D _rig;
  FollowTarget _followTarget;
  private void Awake()
  {
    _rig = GetComponent<Rigidbody2D>();
    _followTarget = GetComponent<FollowTarget>();
  }

  public void Shoot(Vector2 dir, float force)
  {
    _rig.AddForce(dir * force);
  }

  public void FollowTransform(Transform targetTransform)
  {
    _followTarget.Follow(transform);
  }
}
