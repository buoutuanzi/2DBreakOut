using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 我也不知道这些方法该放哪，弄个Utils类包了吧
public class BuffUtils
{
    public static BuffType GetARandomBuffType()
    {
        BuffType[] buffTypes = Enum.GetValues(typeof(BuffType)) as BuffType[];
        int randomBuffIndex = UnityEngine.Random.Range(0, buffTypes.Length);
        BuffType randomBuffType = buffTypes[randomBuffIndex];
        return randomBuffType;
    }

    public static float GetARandomBuffArgs(BuffType buffType)
    {
        if (!BuffGenerationConfig.IsHasConfig(buffType))
        {
            Debug.LogWarning("该Buff没有配置参数生成配置，Type: " + buffType);
            return 0f;
        }

        float[] minMaxConfig = BuffGenerationConfig.GetMinMaxValue(buffType);
        return UnityEngine.Random.Range(minMaxConfig[0], minMaxConfig[minMaxConfig.Length - 1]);
    }
}
