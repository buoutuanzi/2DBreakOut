public interface ObjectPool<T>
{
  public T Spwan();

  public void Return(T t);

  public void Clear();
}
