using UnityEngine;

public class CollisionPannel : MonoBehaviour
{
    BoxCollider2D _collider;
    float originLen = 0;
    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        originLen = _collider.size.x;
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        Collidable collidable = other.gameObject.GetComponent<Collidable>();
        if (collidable != null)
        {
            collidable.OnCollidableExit(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Collidable collidable = other.gameObject.GetComponent<Collidable>();
        if (collidable != null)
        {
            collidable.OnCollidableEnter(this);
        }
    }

    public void SetLenScaleRefOriginLen(float scale)
    {
        _collider.size = new Vector2(originLen * scale, _collider.size.y);
    }
}
