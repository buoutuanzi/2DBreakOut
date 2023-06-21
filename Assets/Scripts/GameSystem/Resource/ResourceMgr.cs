using System.Collections.Generic;
using UnityEngine;

// 防止重复加载，统一通过这个Mgr加载资源
public class ResourceMgr : SingleTon<ResourceMgr>
{
  Dictionary<string, Object> resourceCacheByPath;
  Dictionary<string, int> resourceRefCounter;

  IResourceLoader resourceLoader;

  private void Awake()
  {
    resourceCacheByPath = new Dictionary<string, Object>();
    resourceRefCounter = new Dictionary<string, int>();
    resourceLoader = new ResouceFolderLoader();
  }

  public Object LoadFromPath(string path)
  {
    Object resource = null;
    if (resourceCacheByPath.ContainsKey(path))
    {
      resource = resourceCacheByPath[path];
    }
    else
    {
      resource = resourceLoader.LoadFromPath(path);
      resourceCacheByPath.Add(path, resource);
      resourceRefCounter.Add(path, 0);
    }

    resourceRefCounter[path]++;

    return resource;
  }

  public void ReleaseByPath(string path)
  {
    if (--resourceRefCounter[path] <= 0)
    {
      resourceRefCounter.Remove(path);
      Object resource;
      resourceCacheByPath.Remove(path, out resource);
      resourceLoader.Release(resource);
    }
  }

  void ReleaseAll()
  {
    foreach (var res in resourceCacheByPath)
    {
      ReleaseByPath(res.Key);
    }


  }

  private void OnDestroy()
  {
    ReleaseAll();
  }
}
