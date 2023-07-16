using UnityEngine;

public class Bullet : Collidable, IReuseableItem
{
    Rigidbody2D _rig;
    private float DirScale = 4.0f;
    public BulletShareProperty shareProperty;
    private void Awake()
  {
    _rig = GetComponent<Rigidbody2D>();
  }

  public void Shoot(Vector2 dir)
  {
        float speed = shareProperty.velocity;
        _rig.velocity = dir * speed;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    OnHit();
  }

  private void OnHit()
  {
    return;
  }

    public void Return()
    {
        BulletSpawn.Instance.Return(gameObject);
    }

    public override void OnCollidableExit(CollisionPannel other)
    {
        ReflectBulletByRefPos(other);
    }

    private void ReflectBulletByRefPos(CollisionPannel other)
    {
        float diffX = transform.position.x - other.transform.position.x;
        Vector2 BiasVec = new Vector2(diffX * DirScale, 0);
        Vector2 oldVelocityDir = _rig.velocity;
        float velocityMag = oldVelocityDir.magnitude;
        Vector2 newVelocityDir = (oldVelocityDir + BiasVec).normalized;
        _rig.velocity = newVelocityDir * velocityMag;
    }

    public void RefreshVelocity(){
        _rig.velocity = _rig.velocity.normalized * shareProperty.velocity;
    }
}
