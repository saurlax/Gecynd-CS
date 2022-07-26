using OpenTK.Mathematics;

using Project2398.Common;

namespace Project2398
{
  public class Project2398
  {
    public World World = new World();

    public Project2398()
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