using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollideAttacker : MonoBehaviour
{
    public abstract float GetAttack();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Defender defender = collision.gameObject.GetComponent<Defender>();
        if (defender != null)
        {
            float attack = GetAttack();
            defender.OnHit(attack);
        }
    }
}
