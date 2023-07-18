using System;

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
}
