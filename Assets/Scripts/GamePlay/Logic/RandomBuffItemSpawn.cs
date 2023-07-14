using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

// 随机生成碰到后产生特殊效果的物体
public class RandomBuffItemSpawn : MonoSingleTon<RandomBuffItemSpawn>
{
    const string ItemPath = "Prefabs/BuffItem";
    private GameObject ItemPrefab;
    PrefabObjectPool pool;
    private int defaultItemPoolCapcity = 10;
    protected override void Awake()
    {
        base.Awake();
        LoadPrefab();
        if(ItemPrefab != null)
        {
            pool = new PrefabObjectPool(ItemPrefab, defaultItemPoolCapcity);
        }
    }

    private void LoadPrefab()
    {
        if (ItemPath != null && ItemPath.Length > 0)
        {
            ItemPrefab = (GameObject)ResourceMgr.Instance.LoadFromPath(ItemPath);
        }
    }

    private void ReleasePrefab()
    {
        if (ItemPrefab)
        {
            ItemPrefab = null;
            if (ResourceMgr.hasInstance())
            {
                ResourceMgr.Instance.ReleaseByPath(ItemPath);
            } 
        }
    }

    public void SpawnByPosition(Vector3 pos)
    {
        GameObject go = pool.Spawn();
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
        item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        item.GetComponent<BuffCollectable>().args = null;
        pool.Return(item);
    }

    private void OnApplicationQuit()
    {
        pool.Clear();
        ReleasePrefab();
    }
}
