using UnityEngine;

// 从 Resouces 文件夹加载资源的加载器
public class ResouceFolderLoader : IResourceLoader
{
  public Object LoadFromPath(string path)
  {
    return Resources.Load(path);
  }

  public void Release(Object resource)
  {
    Resources.UnloadUnusedAssets();
  }
}
