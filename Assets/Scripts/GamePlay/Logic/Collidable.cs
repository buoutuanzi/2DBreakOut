using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 可以与CollisionPanel碰撞产生效果的
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
