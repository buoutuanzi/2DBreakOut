using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelItemSpawnChance", menuName = "ScriptableObject/Config/Item/LevelItemSpawnChance")]
public class LevelItemSpawnChance : ScriptableObject
{
    [Range(0f, 1f)]
    public List<float> chanceConfig;
}
