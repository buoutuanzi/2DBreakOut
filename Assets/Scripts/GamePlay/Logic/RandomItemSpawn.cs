using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

// 随机生成碰到后产生特殊效果的物体
public class RandomBuffItemSpawn : SingleTon<RandomBuffItemSpawn>, IObjectPool<GameObject>
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
        go.GetComponent<BuffCollectableVisual>().UpdateVisual();
        go.transform.SetPositionAndRotation(pos, Quaternion.identity);
    }

    private void SetRandomBuff(GameObject item)
    {
        BuffCollectable buffCollectable = item.GetComponent<BuffCollectable>();
        BuffType buffType = BuffUtils.GetARandomBuffType();
        if (BuffGenerationConfig.IsHasConfig(buffType))
        {
            float args = BuffUtils.GetARandomBuffArgs(buffType);
            buffCollectable.args = args;
        }
        buffCollectable.buffType = buffType;
    }
    public void Return(GameObject item)
    {
        item.transform.position = Vector3.zero;
        item.transform.rotation = Quaternion.identity;
        item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        item.GetComponent<BuffCollectable>().args = null;
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
