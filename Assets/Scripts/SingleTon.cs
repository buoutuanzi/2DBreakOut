using UnityEngine;

public class SingleTon<T> : MonoBehaviour where T : SingleTon<T>
{
  private static T _instance;
  public static T Instance
  {
    get
    {
      if (_instance == null)
      {
        GameObject DDOL = GameObject.Find("DDOL");

        if (DDOL == null)
        {
          DDOL = new GameObject("DDOL");
          DontDestroyOnLoad(DDOL);
        }

        _instance = DDOL.GetComponent<T>();
        if (_instance == null)
        {
          _instance = DDOL.AddComponent<T>();
        }
      }

      return _instance;
    }
  }
}
