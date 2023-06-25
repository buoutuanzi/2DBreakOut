using UnityEngine;

// 负责子弹发射相关功能
public class Shooter : MonoBehaviour
{
  [SerializeField]
  private Transform createPoint;
  [SerializeField]
  private float createTime = 3f;
  private float _leftCreateTime = 0f;
  private bool _isWaitingBullet = false;

  [SerializeField]
  private float speedScale = .5f;

  private Bullet _curBullet;
  private Rigidbody2D _rig;
  private Vector2 defaultShootDir = new Vector2(0, 1);
  public float defaultShootForce = 10f;
  private Vector3 _lastFramePos;
  private float collisionHalfWidth = 0;

  private void Awake()
  {
    _rig = GetComponent<Rigidbody2D>();
    collisionHalfWidth = GetComponent<BoxCollider2D>().size.x / 2;
  }

  // Update is called once per frame
  private void Update()
  {
    HandleMovment();
    HandleCreateBullet();
    HandleShoot();
  }

  private void OnDestroy()
  {
    if (_isWaitingBullet)
    {
      EventBus.Instance.UnRegisteTo(EventType.OnBulletCanBeGet, OnBulletCanBeGet);
    }
  }

  private void HandleMovment()
  {
    Vector2 mouseWorldPos = GameUtils.GetMouseWorldPosClampByScreen(collisionHalfWidth / 2, collisionHalfWidth / 2);
    Vector3 oldPos = transform.position;
    Vector3 newPos = new Vector2(mouseWorldPos.x, oldPos.y);
    transform.position = newPos;
    _lastFramePos = oldPos;
  }

  private void HandleCreateBullet()
  {
    if (_isWaitingBullet || _curBullet != null)
    {
      return;
    }

    _leftCreateTime -= Time.deltaTime;
    if (_leftCreateTime <= 0)
    {
      bool isGetBullet = GetBullet();
      if (isGetBullet)
      {
        // 拿到了子弹，重置倒计时
        _leftCreateTime = createTime;
        return;
      }
      else
      {
        EventBus.Instance.RegisteTo(EventType.OnBulletCanBeGet, OnBulletCanBeGet);
        _isWaitingBullet = true;
      }
    }
  }

  private void HandleShoot()
  {
    if (Input.GetButtonDown("Fire1") && _curBullet != null)
    {
      Shoot();
    }
  }

  private void Shoot()
  {
    Vector2 velocity = ((transform.position - _lastFramePos) / Time.deltaTime) * speedScale;
    Vector2 shootDir = (defaultShootDir + velocity).normalized;
    float force = defaultShootForce;
    // 取消子弹跟随
    _curBullet.transform.SetParent(transform.parent);
    _curBullet.Shoot(shootDir, force);
    _curBullet = null;
  }

  private bool GetBullet()
  {
    GameObject bullet = BulletSpwan.Instance.GetAndAttachTo(createPoint);
    if (bullet != null)
    {
      _curBullet = bullet.GetComponent<Bullet>();
      return true;
    }

    return false;
  }

  public void OnBulletCanBeGet(object args)
  {
    EventBus.Instance.UnRegisteTo(EventType.OnBulletCanBeGet, OnBulletCanBeGet);
    GetBullet();
    _isWaitingBullet = false;
  }

}
