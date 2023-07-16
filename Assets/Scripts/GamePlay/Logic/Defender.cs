using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Defender : MonoBehaviour
{
    public float health = 1;

    public abstract void OnHit(float attack);
}
