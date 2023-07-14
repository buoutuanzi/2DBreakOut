using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "buffItemVisualConfig", menuName = "ScriptableObject/Config/Item/BuffItemVisualConfig")]
public class BuffItemVisualConfig : ScriptableObject
{
    public List<BuffItemSingleVisualConfig> configs;
}
