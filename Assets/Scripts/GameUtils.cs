using System;
using System.Reflection;
using UnityEngine;

public class GameUtils
{
  public static Vector2 GetMouseWorldPosClampByScreen(float minBias, float maxBias)
  {
    Camera mainCamera = Camera.main;
    Vector2 mouseScreenPos = Input.mousePosition;
    mouseScreenPos = ClampByScreen(mouseScreenPos);
    Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
    if (minBias != 0 || maxBias != 0)
    {
      float screenLeftPos = mainCamera.ScreenToWorldPoint(Vector3.zero).x;
      float screenRightPos = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
      mouseWorldPos.x = Math.Clamp(mouseWorldPos.x, screenLeftPos + minBias, screenRightPos - maxBias);
    }
    return mouseWorldPos;
  }

  public static Vector2 ClampByScreen(Vector2 pos)
  {
    float screenHeight = Screen.height;
    float screenWidth = Screen.width;

    return new Vector2(Math.Clamp(pos.x, 0.0f, screenWidth), Math.Clamp(pos.y, 0.0f, screenHeight));
  }

  public static void GetAllTypeWithTargetAttribute<T>(Action<Type, T> callback) where T : Attribute
    {
        Type[] classes = Assembly.GetExecutingAssembly().GetTypes();
        foreach(var clazz in classes)
        {
            T targetAttribute = clazz.GetCustomAttribute<T>();
            if (targetAttribute != null)
            {
                callback?.Invoke(clazz, targetAttribute);
            }
        }
    }
}
