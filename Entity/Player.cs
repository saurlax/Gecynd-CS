using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Common;
using OpenTK.Mathematics;

namespace Project2398
{
  public class Player : Entity
  {
    private float speed = 1.5f;
    public Player(Vector3 position) : base(position)
    {
    }
    public void HandleMove(FrameEventArgs e, KeyboardState state)
    {
      if (state.IsKeyDown(Keys.W)) MoveBy(new Vector3(0, 0, 0) * speed * (float)e.Time);
    }
  }
}