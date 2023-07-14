using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffItemBuilder
{
    private GameObject curBuffItem = null;
    private Dictionary<BuffType, BuffItemSingleVisualConfig> buff2VisualConfigMap =
        new Dictionary<BuffType, BuffItemSingleVisualConfig>();
    private BuffCollectable curBuffCollectable;
    private BuffCollectableVisual curBuffCollectableVisual;
    public void SetVisualConfig(BuffItemVisualConfig visualConfig)
    {
        ParseConfig(visualConfig);
    }

    private void ParseConfig(BuffItemVisualConfig visualConfig)
    {
        if (visualConfig)
        {
            foreach (var config in visualConfig.configs)
            {
                buff2VisualConfigMap.Add(config.buffType, config);
            }
        }
    }
    public BuffItemBuilder SetBuffItem(GameObject buffItem)
    {
        curBuffItem = buffItem;
        curBuffCollectable = curBuffItem.GetComponent<BuffCollectable>();
        curBuffCollectableVisual = curBuffItem.GetComponent<BuffCollectableVisual>();
        return this;
    }

    public GameObject Build()
    {
        if(curBuffItem == null)
        {
            return null;
        }
        GameObject item = curBuffItem;
        curBuffItem = null;
        curBuffCollectable = null;
        curBuffCollectableVisual = null;   
            
        return item;
    }

    // 传入参数为空时，随机生成
    public BuffItemBuilder SetBuffType(BuffType? buffType)
    {
        if(curBuffItem != null)
        {
            if (buffType == null)
            {
                buffType = BuffUtils.GetARandomBuffType();
            }

            if(curBuffCollectable != null)
            {
                curBuffCollectable.buffType = buffType.Value;
            }

            if(curBuffCollectableVisual != null)
            {
                BuffItemSingleVisualConfig visualConfig = buff2VisualConfigMap[buffType.Value];
                curBuffCollectableVisual.UpdateVisual(visualConfig);
            }
        }

        return this;
    }

    public BuffItemBuilder SetBuffArgs(object args)
    {
        if (curBuffItem != null && curBuffCollectable != null)
        {
            BuffType buffType = curBuffCollectable.buffType;

            if (args == null)
            {
                args = BuffUtils.GetARandomBuffArgs(buffType);
            }
            curBuffCollectable.args = args;
        }

        return this;
    }
}
