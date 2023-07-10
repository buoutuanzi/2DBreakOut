using System.Collections.Generic;
using UnityEngine;

public class BuffCollectableVisualConfig
{
    static Dictionary<BuffType, Color> _buffType2VisualColor = new Dictionary<BuffType, Color>()
    {
        { BuffType.ChangeBulletVelocity, Color.blue},
        { BuffType.ChangePanelLen, Color.green}
    };

    public static Color DefaultColor { get { return Color.white; } }

    public static bool IsHasConfig(BuffType buffType)
    {
        return _buffType2VisualColor.ContainsKey(buffType);
    }

    public static Color GetVisualColorByBuff(BuffType buff)
    {
        if (!IsHasConfig(buff))
        {
            Debug.LogError("该buff没有配置颜色，Type: " + buff);
            return Color.white;
        }

        return _buffType2VisualColor[buff];
    }
}
