namespace Project2398.Common
{
  public class Sequence3D<T>
  {
    private T? _all;
    private Sequence3D<T>[] _children;
    public void Fill(T value)
    {
      _all = value;
    }

    public T Get(int index)
    {
      if (_all != null) return _all;
      return _children[index].Get(index);
    }
  }
}