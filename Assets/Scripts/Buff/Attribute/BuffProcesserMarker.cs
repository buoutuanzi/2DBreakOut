using System;
/// <summary>
/// ���һ����ΪBuffЧ��������
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
