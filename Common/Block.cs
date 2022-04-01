using OpenTK.Mathematics;

namespace Project2398.Common
{
  public class Block
  {
    public static Block BLOCK = new Block();
    public virtual void Render(Vector4i position, Camera camera) { }
  }
}