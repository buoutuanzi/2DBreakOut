using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "bulletShareProperty", menuName = "ScriptableObject/Property/GamePlay/BulletShareProperty")]
// �ӵ���������
public class BulletShareProperty : ScriptableObject
{
    public float attack;
    public float velocity;
    public float angularVelScale;
    public float dirScale = 4.0f;
    public float maxDeltaDeg = 70;
}
