using OpenTK.Mathematics;
using Project2398.Client;

namespace Project2398.Common
{
  public class Entity
  {
    private Vector3 _position;
    private Camera _camera;
    public Entity(Vector3 position)
    {
      _position = position;
      _camera = new Camera(position, 1f);
    }
    public void MoveBy(Vector3 delta)
    {
      _position += delta;
    }

    public void MoveTo(Vector3 end)
    {
      _position = end;
    }
  }
}