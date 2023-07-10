using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������Buffʱ����ʹ�ô������ļ�����buff�Ĳ�����������
public class BuffGenerationConfig : MonoBehaviour
{
    static Dictionary<BuffType, object> _buffArgsConfig = new Dictionary<BuffType, object>()
    {
        { BuffType.ChangeBullectVelocity, new float[]{0.5f, 2} },
        { BuffType.ChangePanelLen, new float[]{0.5f, 2} },
    };

    public static bool IsHasConfig(BuffType buffType)
    {
        return _buffArgsConfig.ContainsKey(buffType);
    }

    public static float GetMinValue(BuffType buffType)
    {
        float[] minMaxConfig = GetMinMaxValue(buffType);
        return minMaxConfig[0];
    }

    public static float GetMaxValue(BuffType buffType)
    {
        float[] minMaxConfig = GetMinMaxValue(buffType);
        return minMaxConfig[minMaxConfig.Length - 1];
    }

    public static float[] GetMinMaxValue(BuffType buffType)
    {
        if (!_buffArgsConfig.ContainsKey(buffType))
        {
            Debug.LogError("��Buffû�������Type: " + buffType);
            return new float[0];
        }

        float[] minMaxConfig = _buffArgsConfig[buffType] as float[];
        return minMaxConfig;
    }
}
