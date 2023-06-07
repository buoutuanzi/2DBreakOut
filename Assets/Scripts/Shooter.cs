using UnityEngine;

public class Shooter : MonoBehaviour
{

  [SerializeField]
  private GameObject bulletPrefab;

  [SerializeField]
  private Transform createPoint;

  private Bullet _curBullet;
  private Rigidbody2D _rig;
  private Vector2 defaultShootDir = new Vector2(0, 1);
  public float defaultShootForce = 10f;

  private void Awake()
  {
    _rig = GetComponent<Rigidbody2D>();
  }
  // Start is called before the first frame update
  void Start()
  {
    CreateBullet();
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
    Vector2 oldPos = transform.position;
    Vector2 newPos = new Vector2(mouseWorldPos.x, oldPos.y);
    transform.position = newPos;
  }

  void HandleShoot()
  {
    if (Input.GetButtonDown("Fire1"))
    {
      Shoot();
    }
  }

  void Shoot()
  {
    Vector2 velocity = _rig.velocity.normalized;
    Vector2 shootDir = (defaultShootDir + velocity).normalized;
    float force = defaultShootForce;
    // 取消子弹跟随
    _curBullet.transform.SetParent(transform.parent);
    _curBullet.Shoot(shootDir, force);
  }

  void CreateBullet()
  {
    GameObject bullet = Instantiate(bulletPrefab, createPoint);
    _curBullet = bullet.GetComponent<Bullet>();
  }
}
