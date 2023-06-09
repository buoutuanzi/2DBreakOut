using UnityEngine;

public class Shooter : MonoBehaviour
{
  [SerializeField]
  private Transform createPoint;

  [SerializeField]
  private float speedScale = .5f;

  private Bullet _curBullet;
  private Rigidbody2D _rig;
  private Vector2 defaultShootDir = new Vector2(0, 1);
  public float defaultShootForce = 10f;
  private Vector3 _lastFramePos;

  private void Awake()
  {
    _rig = GetComponent<Rigidbody2D>();
  }
  // Start is called before the first frame update
  void Start()
  {
    GetBullet();
  }

  // Update is called once per frame
  void Update()
  {
    HandleMovment();
    HandleShoot();
  }

  void HandleMovment()
  {
    Vector2 mouseWorldPos = GameUtils.GetMouseWorldPosClampByScreen();
    Vector3 oldPos = transform.position;
    Vector3 newPos = new Vector2(mouseWorldPos.x, oldPos.y);
    transform.position = newPos;
    _lastFramePos = oldPos;
  }

  void HandleShoot()
  {
    if (Input.GetButtonDown("Fire1") && _curBullet != null)
    {
      Shoot();
    }
  }

  void Shoot()
  {
    Vector2 velocity = ((transform.position - _lastFramePos) / Time.deltaTime) * speedScale;
    Vector2 shootDir = (defaultShootDir + velocity).normalized;
    Debug.Log(shootDir);
    float force = defaultShootForce;
    // 取消子弹跟随
    _curBullet.transform.SetParent(transform.parent);
    _curBullet.Shoot(shootDir, force);
  }

  void GetBullet()
  {
    GameObject bullet = BulletSpwan.Instance.GetAndAttachTo(createPoint);
    if (bullet != null)
    {
      _curBullet = bullet.GetComponent<Bullet>();
    }
  }
}
