using UnityEngine;

public class Bullet : Collidable, IReuseableItem
{
    Rigidbody2D _rig;
    private float angularVel;
    private BulletVisual bulletVisual;
    CircleCollider2D _collider;
    public BulletShareProperty shareProperty;
    private void Awake()
  {
    _rig = GetComponent<Rigidbody2D>();
    _collider = GetComponent<CircleCollider2D>();
    bulletVisual = GetComponent<BulletVisual>();
  }

  public void Shoot(Vector2 dir)
  {
        float speed = shareProperty.velocity;
        _rig.velocity = dir * speed;
  }

    public void Return()
    {
        BulletSpawn.Instance.Return(gameObject);
    }

    public override void OnCollidableExit(CollisionPannel other)
    {
        ReflectBulletByRefPos(other);
        HandleRotation(other);
    }

    private void ReflectBulletByRefPos(CollisionPannel other)
    {
        float diffX = transform.position.x - other.transform.position.x;
        Vector2 BiasVec = new Vector2(diffX * shareProperty.dirScale, 0);
        Vector2 oldVelocityDir = _rig.velocity;
        float velocityMag = oldVelocityDir.magnitude;
        Vector2 newVelocityDir = (oldVelocityDir + BiasVec).normalized;
        _rig.velocity = newVelocityDir * velocityMag;
    }

    private void HandleRotation(CollisionPannel other)
    {
        ShooterMover mover = other.gameObject.GetComponent<ShooterMover>();
        float diffVelX = mover.Velocity.x - _rig.velocity.x;
        float deltaAngularVel = diffVelX / _collider.radius;
        angularVel += deltaAngularVel;
        UpdateAngularVel(angularVel);
    }

    private void UpdateAngularVel(float angularVel)
    {
        this.angularVel = angularVel;
        bulletVisual.UpdateAngularVel(angularVel);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ChangeReflectDirByAngularVel();
    }

    private void ChangeReflectDirByAngularVel()
    {
        if(angularVel != 0)
        {
            Vector2 originDir = _rig.velocity.normalized;
            float deltaRad = angularVel* shareProperty.angularVelScale;
            float maxDeltaRad = Mathf.Deg2Rad * shareProperty.maxDeltaDeg;
            deltaRad = Mathf.Clamp(deltaRad, -maxDeltaRad, maxDeltaRad);
            Quaternion deltaDir = Quaternion.Euler(0,0, Mathf.Rad2Deg * deltaRad);
            Vector2 newDir = deltaDir * originDir;
            _rig.velocity = newDir * _rig.velocity.magnitude;
            Debug.LogFormat("原方向{0}，新方向{1}，改变角度{2}", originDir, newDir, Mathf.Rad2Deg * deltaRad);
        }
    }

    public void RefreshVelocity(){
        _rig.velocity = _rig.velocity.normalized * shareProperty.velocity;
    }

    private void OnDisable()
    {
        UpdateAngularVel(0);
    }
}
