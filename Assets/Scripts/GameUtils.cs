using System;
using UnityEngine;

public class GameUtils
{
  public static Vector2 GetMouseWorldPosClampByScreen()
  {
    Camera mainCamera = Camera.main;
    Vector2 mouseScreenPos = Input.mousePosition;
    mouseScreenPos = ClampByScreen(mouseScreenPos);
    Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
    return mouseWorldPos;
  }

  public static Vector2 ClampByScreen(Vector2 pos)
  {
    float screenHeight = Screen.height;
    float screenWidth = Screen.width;

    return new Vector2(Math.Clamp(pos.x, 0.0f, screenWidth), Math.Clamp(pos.y, 0.0f, screenHeight));
  }


}
