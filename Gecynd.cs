using OpenTK.Mathematics;

using Gecynd.Common;

namespace Gecynd
{
  public class Gecynd
  {
    public World World = new World();

    public Gecynd()
    {
      Block testBlock = new Block("test-block");
      World.LoadChunk(Vector3i.Zero);
      World.SetBlock(new Vector3i(8, 15, 8), testBlock);
      World.SetBlock(Vector3i.Zero, testBlock);
    }

    public void LoadMod()
    {
      // Mod.LoadAll();
    }
  }
}