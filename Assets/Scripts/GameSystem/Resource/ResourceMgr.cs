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
    if (!_resourceCacheByPath.ContainsKey(path))
    {
        return;
    }

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
    List<Object> resourceToRelease = new List<Object>();
    foreach (var res in _resourceCacheByPath)
    {
      resourceToRelease.Add(res.Value);
    }
    foreach(var res in resourceToRelease)
    {
        _resourceLoader.Release(res);
    }

    _resourceCacheByPath.Clear();
    _resourceRefCounter.Clear();
  }

  private void OnDestroy()
  {
    ReleaseAll();
  }
}
