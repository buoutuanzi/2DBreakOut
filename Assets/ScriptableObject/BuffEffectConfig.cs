using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "buffEffectConfig", menuName = "ScriptableObject/Config/Buff/BuffEffectConfig")]
public class BuffEffectConfig : ScriptableObject
{
    public List<BuffEffectSingleConfig> effectConfigs;
}
