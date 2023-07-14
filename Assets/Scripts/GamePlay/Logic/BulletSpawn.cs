using UnityEngine;
using System.Collections.Generic;

public class BulletSpawn : SingleTon<BulletSpawn>
{
  private int defaultBulletPoolCapcity = 10;

  private PrefabObjectPool pool;
  string prefabPath = "Prefabs/Bullet";
  GameObject bulletPrefab;

  public HashSet<GameObject> activeBulletSet = new HashSet<GameObject>();

    private void Awake()
    {
        EventBus.Instance.RegisteTo(EventType.OnLevelComplete, CollectAllActiveBullet);
        LoadPrefab();
        if(bulletPrefab != null)
        {
            pool = new PrefabObjectPool(bulletPrefab, defaultBulletPoolCapcity);
        }
    }

    private void CollectAllActiveBullet(object args)
    {
        List<GameObject> toBeCollected = new List<GameObject>();
        foreach(GameObject bullet in activeBulletSet)
        {
            toBeCollected.Add(bullet);
        }
        for(int i = 0; i < toBeCollected.Count; i++)
        {
            Return(toBeCollected[i]);
        }
        activeBulletSet.Clear();
    }

    public GameObject GetAndAttachTo(Transform parentTransform)
  {
    GameObject bullet = pool.Spawn();
    if (bullet != null)
    {
      bullet.transform.SetParent(parentTransform);
      bullet.transform.localPosition = Vector3.zero;
      bullet.transform.localRotation = Quaternion.identity;
      activeBulletSet.Add(bullet);
    }

    return bullet;
  }

  void LoadPrefab()
  {
    if (prefabPath != null && prefabPath.Length > 0)
    {
      bulletPrefab = (GameObject)ResourceMgr.Instance.LoadFromPath(prefabPath);
    }

  }

  void ReleasePrefab()
  {
    if (bulletPrefab)
    {
      bulletPrefab = null;
        if (ResourceMgr.hasInstance())
        {
            ResourceMgr.Instance.ReleaseByPath(prefabPath);
        }
    }
  }

  public void Return(GameObject bullet)
  {
    bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    activeBulletSet.Remove(bullet);
    pool.Return(bullet);

    EventBus.Instance.TriggerEvent(EventType.OnBulletCanBeGet, null);
  }

  private void OnApplicationQuit()
  {
    ClearBullet();
    ReleasePrefab();
  }

  private void ClearBullet()
  {
    // 清除已激活
    GameObject[] toBeDestroy = new GameObject[activeBulletSet.Count];
    int index = 0;
    foreach (GameObject bullet in activeBulletSet)
    {
        toBeDestroy[index++] = bullet;
    }
    for (int i = 0; i < toBeDestroy.Length; i++)
    {
        DestroyImmediate(toBeDestroy[i]);
    }
    // 清空对象池
    pool.Clear();
  }
}
