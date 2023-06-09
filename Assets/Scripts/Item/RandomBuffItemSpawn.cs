using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

// 随机生成碰到后产生特殊效果的物体
public class RandomBuffItemSpawn : MonoSingleTon<RandomBuffItemSpawn>
{
    public BuffItemVisualConfig visualConfig;
    public LevelItemSpawnChance levelChanceConfig;
    private Dictionary<BuffType, BuffItemSingleVisualConfig> buff2VisualConfigMap = 
        new Dictionary<BuffType, BuffItemSingleVisualConfig>();
    const string ItemPath = "Prefabs/BuffItem";
    private GameObject ItemPrefab;
    PrefabObjectPool pool;
    private int defaultItemPoolCapcity = 10;
    private BuffItemBuilder buffItemBuilder = new BuffItemBuilder();
    protected override void Awake()
    {
        base.Awake();
        buffItemBuilder.SetVisualConfig(visualConfig);
        //ParseConfig();
        LoadPrefab();
        if(ItemPrefab != null)
        {
            pool = new PrefabObjectPool(ItemPrefab, defaultItemPoolCapcity);
        }
    }

    private void ParseConfig()
    {
        if (visualConfig)
        {
            foreach(var config in visualConfig.configs)
            {
                buff2VisualConfigMap.Add(config.buffType, config);
            }
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
        pool.Clear();
        ReleasePrefab();
    }
}
