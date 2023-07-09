using UnityEngine;
using System.Collections.Generic;

public class BulletSpawn : SingleTon<BulletSpawn>, IObjectPool<GameObject>
{
  int canCreate = 10;

  Queue<GameObject> pool = new Queue<GameObject>();
  string prefabPath = "Prefabs/Bullet";
  GameObject bulletPrefab;

  public HashSet<GameObject> activeBulletSet = new HashSet<GameObject>();

  public GameObject GetAndAttachTo(Transform parentTransform)
  {
    GameObject bullet = Spawn();
    if (bullet != null)
    {
      bullet.transform.SetParent(parentTransform);
      bullet.transform.localPosition = Vector3.zero;
      bullet.transform.localRotation = Quaternion.identity;
    }

    return bullet;
  }

  public GameObject Spawn()
  {
    if (pool.Count > 0)
    {
      GameObject bullet = pool.Dequeue();
      bullet.SetActive(true);
      activeBulletSet.Add(bullet);
      return bullet;
    }

    if (canCreate > 0)
    {
      if (bulletPrefab == null)
      {
        LoadPrefab();
      }
      if (bulletPrefab != null)
      {
        GameObject go = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
        canCreate--;
        activeBulletSet.Add(go);
        return go;
      }

    }

    return null;
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
      ResourceMgr.Instance.ReleaseByPath(prefabPath);
    }
  }

  public void Return(GameObject bullet)
  {
    bullet.transform.position = Vector3.zero;
    bullet.transform.rotation = Quaternion.identity;
    bullet.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    bullet.SetActive(false);
    activeBulletSet.Remove(bullet);
    pool.Enqueue(bullet);

    EventBus.Instance.TriggerEvent(EventType.OnBulletCanBeGet, null);
  }

  public void Clear()
  {
    foreach(var bullet in activeBulletSet)
    {
        DestroyImmediate(bullet);
    }
    while (pool.Count > 0)
    {
      GameObject go = pool.Dequeue();
      DestroyImmediate(go);
    }
  }

  private void OnDestroy()
  {
    Clear();
    ReleasePrefab();
  }
}
