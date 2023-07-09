using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItemSpawn : SingleTon<RandomItemSpawn>, IObjectPool<GameObject>
{
    const string ItemPath = "Prefabs/BuffItem";
    private GameObject ItemPrefab;
    Queue<GameObject> pool = new Queue<GameObject>();
    private void Awake()
    {
        LoadPrefab();
    }

    void LoadPrefab()
    {
        if (ItemPath != null && ItemPath.Length > 0)
        {
            ItemPrefab = (GameObject)ResourceMgr.Instance.LoadFromPath(ItemPath);
        }

    }

    void ReleasePrefab()
    {
        if (ItemPrefab)
        {
            ItemPrefab = null;
            ResourceMgr.Instance.ReleaseByPath(ItemPath);
        }
    }

    public GameObject Spawn()
    {
        if (pool.Count > 0)
        {
            GameObject item = pool.Dequeue();
            item.SetActive(true);
            return item;
        }
        if (ItemPrefab == null)
        {
            LoadPrefab();
        }
        if (ItemPrefab != null)
        {
            GameObject go = Instantiate(ItemPrefab, Vector3.zero, Quaternion.identity);
            return go;
        }

        return null;
    }

    public void SpawnByPosition(Vector3 pos)
    {
        GameObject go = Spawn();
        SetRandomBuff(go);
        go.transform.SetPositionAndRotation(pos, Quaternion.identity);
    }

    private void SetRandomBuff(GameObject item)
    {
        BuffCollectable buffCollectable = item.GetComponent<BuffCollectable>();
        buffCollectable.buffType = BuffType.ChangePanelLen;
        buffCollectable.args = 2f;
    }
    public void Return(GameObject item)
    {
        item.transform.position = Vector3.zero;
        item.transform.rotation = Quaternion.identity;
        item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        item.SetActive(false);
        pool.Enqueue(item);
    }

    public void Clear()
    {
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
