using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEngine;

// ��Ҫ�ֶ���һ��GameObject��
public class MonoSingleTon<T> : MonoBehaviour where T : MonoSingleTon<T>
{
    public static T Instance { get { return instance; } }
    private static T instance;
    public static bool hasInstance()
    {
        return instance != null;
    }

    protected virtual void Awake()
    {
        if(instance == null)
        {
            instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
}
