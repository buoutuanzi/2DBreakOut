public interface IObjectPool<T>
{
  public T Spawn();

  public void Return(T t);

  public void Clear();
}
