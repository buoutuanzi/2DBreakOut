using System.Collections.Generic;
using UnityEngine;

public class PrefabObjectPool
{
    private GameObject prefab;
    private int capcity;
    Queue<GameObject> pool = new Queue<GameObject> ();
    List<GameObject> activeList = new List<GameObject>();
    public PrefabObjectPool(GameObject prefab, int capcity)
    {
        this.prefab = prefab;
        this.capcity = capcity;
    }
  public GameObject Spawn()
    {
        if (pool.Count > 0)
        {
            GameObject item = pool.Dequeue();
            item.SetActive(true);
            activeList.Add(item);
            return item;
        }
        if (prefab != null)
        {
            GameObject go = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            Object.DontDestroyOnLoad(go);
            activeList.Add(go);
            return go;
        }
        return null;
    }

  public void Return(GameObject t)
    {
        t.transform.position = Vector3.zero;
        t.transform.rotation = Quaternion.identity;
        t.SetActive(false);
        activeList.Remove(t);
        if (pool.Count > capcity)
        {
            GameObject.Destroy(t);
        }
        else
        {
            pool.Enqueue(t);
        }
    }

    public void RestoreAllActive()
    {
        while(activeList.Count > 0)
        {
            GameObject item = activeList[0];
            Return(item);
        }
    }

  public void Clear()
    {
        foreach(GameObject item in activeList)
        {
            GameObject.DestroyImmediate(item);
        }
        while(pool.Count > 0)
        {
            GameObject go = pool.Dequeue();
            GameObject.DestroyImmediate(go);
        }
        pool.Clear();
    }
}
