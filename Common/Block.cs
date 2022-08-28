using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

using Gecynd.Client;

namespace Gecynd.Common
{
  public class Block
  {
    public static Block NULL = new Block("null");

    public String Name { get; }

    public Block(String name)
    {
      Name = name;
    }
  }
}