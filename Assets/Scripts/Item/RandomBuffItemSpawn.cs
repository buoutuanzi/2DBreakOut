using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 随机生成碰到后产生特殊效果的物体
public class RandomBuffItemSpawn : MonoSingleTon<RandomBuffItemSpawn>
{
    public BuffItemVisualConfig visualConfig;
    public LevelItemSpawnChance levelChanceConfig;
    public BuffEffectConfig buffEffectConfig;
    const string ItemPath = "Prefabs/BuffItem";
    private GameObject ItemPrefab;
    PrefabObjectPool pool;
    private int defaultItemPoolCapcity = 10;
    private BuffItemBuilder buffItemBuilder = new BuffItemBuilder();
    protected override void Awake()
    {
        base.Awake();
        buffItemBuilder.SetVisualConfig(visualConfig);
        buffItemBuilder.SetEffectConfig(buffEffectConfig);
        LoadPrefab();
        if(ItemPrefab != null)
        {
            pool = new PrefabObjectPool(ItemPrefab, defaultItemPoolCapcity);
        }
        EventBus.Instance.RegisteTo(EventType.OnItemSpawn, SpawnByPosition);
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

    public void SpawnByPosition(object args)
    {
        Transform targetTransform = args as Transform;
        Vector3 pos = targetTransform.position;
        if (CheckSpawn())
        {
            GameObject go = pool.Spawn();
            buffItemBuilder.SetBuffItem(go)
                .SetBuffType(null)
                .SetBuffArgs(null)
                .Build();
            go.transform.SetPositionAndRotation(pos, Quaternion.identity);
        }
    }

    private bool CheckSpawn()
    {
        float curLevelChance = 0f;
        List<float> chanceConfig = levelChanceConfig.chanceConfig;
        int configIndex = chanceConfig.Count - 1;
        if (LevelMgr.Instance.CurLevel <= chanceConfig.Count)
        {
            configIndex = LevelMgr.Instance.CurLevel - 1;
        }
        if(configIndex < 0) configIndex = 0;
        curLevelChance = chanceConfig[configIndex];
        return UnityEngine.Random.Range(0f, 1f) < curLevelChance;
    }

    public void Return(GameObject item)
    {
        item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        item.GetComponent<BuffCollectable>().args = null;
        pool.Return(item);
    }

    private void OnApplicationQuit()
    {
        if (EventBus.hasInstance())
        {
            EventBus.Instance.RegisteTo(EventType.OnItemSpawn, SpawnByPosition);
        }
        pool.Clear();
        ReleasePrefab();
    }
}
