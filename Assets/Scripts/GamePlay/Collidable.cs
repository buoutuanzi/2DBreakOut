using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������CollisionPanel��ײ����Ч����
public abstract class Collidable : MonoBehaviour
{
    public virtual void OnCollidableExit(CollisionPannel other) 
    {
        return;
    }
    public virtual void OnCollidableEnter(CollisionPannel other)
    { 
        return;
    }
}
