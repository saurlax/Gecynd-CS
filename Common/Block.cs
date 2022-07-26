using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;

using Project2398.Client;

namespace Project2398.Common
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