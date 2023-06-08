using UnityEngine;
using System.Collections.Generic;

public class BulletSpwan : SingleTon<BulletSpwan>, ObjectPool<GameObject>
{
  int canCreate = 10;

  Queue<GameObject> pool = new Queue<GameObject>();
  string prefabPath = "Prefabs/Bullet";
  GameObject bulletPrefab;

  public GameObject GetAndAttachTo(Transform parentTransform)
  {
    GameObject bullet = Spwan();
    if (bullet != null)
    {
      bullet.transform.SetParent(parentTransform);
      bullet.transform.localPosition = Vector3.zero;
      bullet.transform.localRotation = Quaternion.identity;
    }

    return bullet;

  }

  public GameObject Spwan()
  {
    if (pool.Count > 0)
    {
      return pool.Dequeue();
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
        return go;
      }

    }

    return null;
  }

  void LoadPrefab()
  {
    if (prefabPath != null && prefabPath.Length > 0)
    {
      bulletPrefab = (GameObject)Resources.Load(prefabPath);
    }

  }

  public void Return(GameObject bullet)
  {
    pool.Enqueue(bullet);
  }

  public void Clear()
  {
    while (pool.Count > 0)
    {
      GameObject go = pool.Dequeue();
      DestroyImmediate(go);
    }
  }
}
