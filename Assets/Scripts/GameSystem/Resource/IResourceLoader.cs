using UnityEngine;

public interface IResourceLoader
{
  public Object LoadFromPath(string path);
  public void Release(Object resource);
}
