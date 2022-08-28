using OpenTK.Mathematics;
using Gecynd.Client;

namespace Gecynd.Common
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