using System.Collections.Generic;
using UnityEngine;

// 防止重复加载，统一通过这个Mgr加载资源
public class ResourceMgr : SingleTon<ResourceMgr>
{
  private Dictionary<string, Object> _resourceCacheByPath;
  private Dictionary<string, int> _resourceRefCounter;

  private IResourceLoader _resourceLoader;

  private void Awake()
  {
    _resourceCacheByPath = new Dictionary<string, Object>();
    _resourceRefCounter = new Dictionary<string, int>();
    _resourceLoader = new ResouceFolderLoader();
  }

  public Object LoadFromPath(string path)
  {
    Object resource = null;
    if (_resourceCacheByPath.ContainsKey(path))
    {
      resource = _resourceCacheByPath[path];
    }
    else
    {
      resource = _resourceLoader.LoadFromPath(path);
      _resourceCacheByPath.Add(path, resource);
      _resourceRefCounter.Add(path, 0);
    }

    _resourceRefCounter[path]++;

    return resource;
  }

  public void ReleaseByPath(string path)
  {
    if (--_resourceRefCounter[path] <= 0)
    {
      _resourceRefCounter.Remove(path);
      Object resource;
      _resourceCacheByPath.Remove(path, out resource);
      _resourceLoader.Release(resource);
    }
  }

  void ReleaseAll()
  {
    foreach (var res in _resourceCacheByPath)
    {
      ReleaseByPath(res.Key);
    }


  }

  private void OnDestroy()
  {
    ReleaseAll();
  }
}
