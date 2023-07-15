using System;
/// <summary>
/// 标记一个类为Buff效果处理类
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class BuffProcesserMarker : Attribute
{
    public BuffType BuffType { get; private set; }

    public BuffProcesserMarker(BuffType buffType)
    {
        BuffType = buffType;
    }

}
