using System;

// ��Ҳ��֪����Щ�����÷��ģ�Ū��Utils����˰�
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
